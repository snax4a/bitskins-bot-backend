using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Options;
using WebApi.Helpers;
using System.Text;

namespace WebApi.Services
{
    public interface ICsgobackpackService
    {
        Task UpdateItemsPrices();
        Task<ItemPrice> GetItemPrice(string itemName);
    }

    public class CsgobackpackService : ICsgobackpackService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CsgobackpackService> _logger;
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;

        public CsgobackpackService(
            IMapper mapper,
            ILogger<CsgobackpackService> logger,
            HttpClient client,
            IOptions<AppSettings> appSettings)
        {
            _mapper = mapper;
            _logger = logger;
            client.BaseAddress = new Uri("http://csgobackpack.net/api/");
            _httpClient = client;
            _appSettings = appSettings.Value;
        }

        public Task UpdateItemsPrices()
        {
            // var response = await _httpClient.GetAsync("GetItemsList/v2?currency=USD&no_details=true");

            // response.EnsureSuccessStatusCode();

            // var jsonString = await response.Content.ReadAsStringAsync();
            // var parsedJson = JObject.Parse(jsonString);
            // var data = parsedJson["items_list"].ToString();

            // dynamic items = JsonConvert.DeserializeObject(data);

            // int counter = 0;

            // foreach (var item in items)
            // {
            //     if (counter == 10) return;
            //     Console.WriteLine("{0}\n", item.name);
            //     counter++;
            // }

            throw new NotImplementedException();
        }

        public async Task<ItemPrice> GetItemPrice(string itemName)
        {
            StringBuilder url = new StringBuilder("GetItemPrice");
            url.Append($"?key={_appSettings.CsgobackpackApiKey}");
            url.Append($"&id={itemName}");
            url.Append("&currency=USD");
            url.Append("&time=30");

            var response = await _httpClient.GetAsync(url.ToString());

            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ItemPrice>(jsonString);
        }
    }
}
