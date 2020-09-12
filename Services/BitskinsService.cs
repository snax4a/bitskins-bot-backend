using AutoMapper;
using WebApi.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OtpNet;
using System;
using System.Net.Http;
using System.Text.Json;
using Albireo.Base32;
using System.Threading.Tasks;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace WebApi.Services
{
    public interface IBitskinsService
    {
        Task<AccountBalance> GetAccountBalance();
    }

    public class BitskinsService : IBitskinsService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<BitskinsService> _logger;
        private readonly AppSettings _appSettings;
        private readonly HttpClient _httpClient;

        public BitskinsService(
            IMapper mapper,
            ILogger<BitskinsService> logger,
            IOptions<AppSettings> appSettings,
            HttpClient client)
        {
            _mapper = mapper;
            _logger = logger;
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

        // helper methods

        public string Get2FACode()
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
