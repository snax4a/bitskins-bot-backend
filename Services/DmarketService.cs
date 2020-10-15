using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using WebApi.Helpers;
using WebApi.Models.Dmarket;
using System.Net;
using Newtonsoft.Json.Linq;
using WebApi.Entities;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace WebApi.Services
{
    public interface IDmarketService
    {
        Task<BalanceResponse> GetAccountBalance();
    }

    public class DmarketService : IDmarketService
    {
        const string _userAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.102 Safari/537.36";
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly ILogger<DmarketService> _logger;
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;
        private readonly IHttpContextAccessor _accessor;
        private readonly string _baseAddress = "https://api.dmarket.com";

        public DmarketService(
            IMapper mapper,
            DataContext context,
            ILogger<DmarketService> logger,
            HttpClient client,
            IOptions<AppSettings> appSettings,
            IHttpContextAccessor accessor)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
            _httpClient = client;
            _appSettings = appSettings.Value;
            _accessor = accessor;
        }

        public async Task<BalanceResponse> GetAccountBalance()
        {
            int tries = 0;

            do
            {
                tries++;

                var requestUrl = $"{_baseAddress}/account/v1/balance";
                var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                AddRequestHeaders(request.Headers);

                var response = await _httpClient.SendAsync(request);
                string contentString = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("Dmarket account balance response: {content}", contentString);

                if (!response.IsSuccessStatusCode)
                {
                    if(response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        await Authenticate();
                        continue;
                    }
                }

                return JsonConvert.DeserializeObject<BalanceResponse>(contentString);
            }
            while (tries < 2);

            throw new Exception("Could not get account balance");
        }

        // helper methods

        private Account GetAccount() => (Account)_accessor.HttpContext.Items["Account"];

        private void AddRequestHeaders(HttpRequestHeaders headers, bool withAuthorization = true)
        {
            headers.Add("Accept", "application/json, text/plain, */*");
            headers.Add("User-Agent", _userAgent);
            headers.Add("Language", "EN");

            if (withAuthorization)
            {
                string authToken = GetAccount().Settings.DmarketAuthToken;
                headers.Add("Authorization", authToken);
            }
        }

        private async Task Authenticate()
        {
            await Task.Delay(1000);

            var requestUrl = $"{_baseAddress}/marketplace-api/v1/sign-in";
            var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            AddRequestHeaders(request.Headers, false);
            request.Content = new JsonContent(new {
                Email = _appSettings.DmarketEmail,
                Password = _appSettings.DmarketPassword,
                ReCaptchaCode = ""
            });

            var response = await _httpClient.SendAsync(request);
            await response.EnsureSuccessStatusCodeAsync();
            string contentString = await response.Content.ReadAsStringAsync();

            _logger.LogInformation("Dmarket sign-in response: {content}", contentString);

            JObject jObject = JObject.Parse(contentString);
            JToken authToken = jObject.SelectToken("$.AuthToken");
            JToken result = jObject.SelectToken("$.Result");

            if (result == null || result.ToString() != "Authorized" || authToken == null)
                throw new Exception("Incorrect sign-in response");

            var account = GetAccount();
            account.Settings.DmarketAuthToken = authToken.ToString();
            _context.Update(account);
            _context.SaveChanges();
        }
    }
}
