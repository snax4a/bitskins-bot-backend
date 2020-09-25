using AutoMapper;
using WebApi.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OtpNet;
using System;
using System.Net.Http;
using Albireo.Base32;
using System.Threading.Tasks;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using WebApi.Models.Bitskins;
using System.Globalization;
using WebApi.Entities;
using System.Linq;

namespace WebApi.Services
{
    public interface IBitskinsService
    {
        Task<AccountBalance> GetAccountBalance();
        Task<IEnumerable<Sale>> GetRecentSalesInfo(string itemName);
        Task<IEnumerable<Sale>> GetBuyHistory(int pageNumber = 1);
        Task<AccountInventory> GetAccountInventory(int pageNumber = 1);
        Task BuyItem(BitskinsItem item, int accountId);
        Task ProcessItems(List<BitskinsItem> items, int accountId);
    }

    public class BitskinsService : IBitskinsService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly ILogger<BitskinsService> _logger;
        private readonly IWhitelistedItemService _whitelistedItemService;
        private readonly ICsgobackpackService _csgobackpackService;
        private readonly AppSettings _appSettings;
        private readonly HttpClient _httpClient;

        public BitskinsService(
            IMapper mapper,
            DataContext context,
            ILogger<BitskinsService> logger,
            IOptions<AppSettings> appSettings,
            IWhitelistedItemService whitelistedItemService,
            ICsgobackpackService csgobackpackService,
            HttpClient client)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
            _whitelistedItemService = whitelistedItemService;
            _csgobackpackService = csgobackpackService;
            _appSettings = appSettings.Value;
            client.BaseAddress = new Uri("https://bitskins.com/api/v1/");
            _httpClient = client;
        }

        public async Task<AccountBalance> GetAccountBalance()
        {
            string url = PrepareUrl("get_account_balance");
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var parsedJson = JObject.Parse(jsonString);
            var balanceData = parsedJson["data"].ToString();

            return JsonConvert.DeserializeObject<AccountBalance>(balanceData);
        }

        public async Task<IEnumerable<Sale>> GetBuyHistory(int pageNumber = 1)
        {
            var options = new {
                page = pageNumber,
            };

            string url = PrepareUrl("get_buy_history", options);
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var parsedJson = JObject.Parse(jsonString);
            var buyHistory = parsedJson["data"]["items"].ToString();

            return JsonConvert.DeserializeObject<IEnumerable<Sale>>(buyHistory);
        }

        public async Task<AccountInventory> GetAccountInventory(int pageNumber = 1)
        {
            var options = new {
                page = pageNumber,
            };

            string url = PrepareUrl("get_my_inventory", options);
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var parsedJson = JObject.Parse(jsonString);
            var buyHistory = parsedJson["data"].ToString();

            return JsonConvert.DeserializeObject<AccountInventory>(buyHistory);
        }

        public async Task<IEnumerable<Sale>> GetRecentSalesInfo(string itemName)
        {
            if (itemName == null) throw new AppException("You have to provide correct item name.");

            var options = new {
                market_hash_name = itemName,
            };

            string url = PrepareUrl("get_sales_info", options);
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var parsedJson = JObject.Parse(jsonString);
            var itemSales = parsedJson["data"]["sales"].ToString();

            return JsonConvert.DeserializeObject<IEnumerable<Sale>>(itemSales);
        }

        public async Task BuyItem(BitskinsItem item, int accountId)
        {
            _logger.LogInformation("Buying item: {@item}", item);

            var options = new {
                item_ids = item.ItemId,
                prices = item.Price.ToString(new CultureInfo("en-US")),
                auto_trade = "false"
            };

            string url = PrepareUrl("buy_item", options);
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(content);

            if (!response.IsSuccessStatusCode)
            {
                JToken unavailableIds = json.SelectToken("data.unavailable_item_ids");

                if (unavailableIds != null)
                    throw new ItemNotAvailableException(unavailableIds.ToString());
                else
                    throw new ShouldNotifyException($"Buy Item response error: {content}");
            }

            _logger.LogInformation($"SUCCESS ---->> Buy Item response: {content}");

            JToken dataItems = json.SelectToken("$.data.items");

            if (dataItems == null)
                throw new Exception($"Buy item response error - data.items is null : {content}");

            BuyItemResponse[] purchasedItems = dataItems.ToObject<BuyItemResponse[]>();

            foreach (BuyItemResponse i in purchasedItems)
            {
                PurchasedItem purchasedItem = _mapper.Map<PurchasedItem>(i);
                purchasedItem.AccountId = accountId;

                _context.PurchasedItems.Add(purchasedItem);
            }
        }

        public async Task ProcessItems(List<BitskinsItem> items, int accountId)
        {
            var account = _context.Accounts.Find(accountId);
            if (account == null) throw new KeyNotFoundException("Account not found");
            if (account.BotStatus == BotStatus.Off) return;

            _logger.LogInformation("Processing {count} new items", items.Count);

            List<BitskinsItem> wantedItems = await GetWantedItems(items, accountId);

            _logger.LogInformation("Found {count} wanted items.", wantedItems.Count);
            if (wantedItems.Count == 0) return;

            foreach (BitskinsItem item in wantedItems)
            {
                try
                {
                    await BuyItem(item, accountId);
                }
                catch(ItemNotAvailableException ex)
                {
                    _logger.LogWarning("Item is not available for purchase: {message}", ex.Message);
                }
            }
        }

        // helper methods

        private async Task<List<BitskinsItem>> GetWantedItems(List<BitskinsItem> items, int accountId)
        {
            List<BitskinsItem> wantedItems = new List<BitskinsItem>();

            AccountInventory inventory = await GetAccountInventory();

            foreach (BitskinsItem item in items)
            {
                _logger.LogInformation("Checking item price: {@item})", item);
                // check if this item is whitelisted in database
                var whitelisted = _whitelistedItemService.GetByNameAndAccountId(item.MarketHashName, accountId);

                // skip if item is not whitelisted
                if (whitelisted == null)
                {
                    _logger.LogInformation("Skipping - item not whitelisted");
                    continue;
                }

                if (whitelisted.MaxQuantity != 0)
                {
                    var itemsCount = inventory.PendingWithdrawal.Items
                        .Where(i => i.MarketHashName == item.MarketHashName)
                        .Count();

                    if (itemsCount >= whitelisted.MaxQuantity)
                    {
                        var message = "Skipping - pending inventory already contains {c} same items and maxQuantity is set to: {q}";
                        _logger.LogInformation(message, itemsCount, whitelisted.MaxQuantity);
                        continue;
                    }
                }

                // get item external price
                ItemPrice externalPrice = await _csgobackpackService.GetItemPrice(item.MarketHashName);
                decimal averagePrice = Convert.ToDecimal(externalPrice.Average);

                // skip if item price is greather than multiplier * averagePrice
                if (item.Price > (whitelisted.PriceMultiplier * averagePrice))
                {
                    _logger.LogInformation($"Skipping item: {item.Price} > {whitelisted.PriceMultiplier} * {averagePrice}");
                    continue;
                }

                // if above conditions are met add item to wantedItems list
                wantedItems.Add(item);
            }

            return wantedItems;
        }

        private string Get2FACode()
        {
            var secretKey = Base32.Decode(_appSettings.BitskinsSecret);
            var totp = new Totp(secretKey);
            return totp.ComputeTotp(DateTime.UtcNow);
        }

        private string PrepareUrl(string pathName, object options = null)
        {
            string apiKey = _appSettings.BitskinsApiKey;
            string code = Get2FACode();

            StringBuilder url = new StringBuilder(pathName);
            url.Append($"?api_key={apiKey}");
            url.Append($"&code={code}");
            url.Append($"&app_id=730");

            if (options != null)
            {
                PropertyInfo[] properties = options.GetType().GetProperties();

                // iterate over options properties
                foreach (var prop in properties)
                {
                    url.Append(
                        string.Format("&{0}={1}",
                            prop.Name,
                            prop.GetValue(options, null)
                        )
                    );
                }
            }

            // prepare and return correct query string
            return url.ToString();
        }
    }
}
