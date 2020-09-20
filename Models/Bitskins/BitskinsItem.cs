using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebApi.Models.Bitskins
{
    public class BitskinsItem
    {
        [Required]
        [JsonProperty("app_id")]
        public string AppId { get; set; }

        [Required]
        [JsonProperty("item_id")]
        public string ItemId { get; set; }

        [Required]
        [JsonProperty("asset_id")]
        public string AssetId { get; set; }

        [Required]
        [JsonProperty("class_id")]
        public string ClassId { get; set; }

        [Required]
        [JsonProperty("instance_id")]
        public string InstanceId { get; set; }

        public string Image { get; set; }

        [Required]
        [JsonProperty("market_hash_name")]
        public string MarketHashName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Discount { get; set; }

        [Required]
        [JsonProperty("withdrawable_at")]
        [JsonConverter(typeof(UnixTimestampConverter))]
        public DateTime WithdrawableAt { get; set; }

        [Required]
        [JsonProperty("event_type")]
        public string EventType { get; set; }

        public override string ToString()
        {
            return $"Name: {this.MarketHashName}, Price: {this.Price}, Discount: {this.Discount}, ItemId: {this.ItemId}, AssetId: {this.AssetId} ";
        }
    }
}
