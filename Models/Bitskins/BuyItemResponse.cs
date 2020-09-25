using System;
using Newtonsoft.Json;

namespace WebApi.Models.Bitskins
{
    public class BuyItemResponse
    {
        [JsonProperty("app_id")]
        public string AppId { get; set; }

        [JsonProperty("context_id")]
        public string ContextId { get; set; }

        [JsonProperty("item_id")]
        public string ItemId { get; set; }

        [JsonProperty("asset_id")]
        public string AssetId { get; set; }

        [JsonProperty("class_id")]
        public string ClassId { get; set; }

        [JsonProperty("instance_id")]
        public string InstanceId { get; set; }

        [JsonProperty("market_hash_name")]
        public string MarketHashName { get; set; }
        public decimal Price { get; set; }

        [JsonProperty("withdrawable_at")]
        [JsonConverter(typeof(UnixTimestampConverter))]
        public DateTime WithdrawableAt { get; set; }
    }
}
