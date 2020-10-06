using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebApi.Models.Bitskins
{
    public class BuyOrder
    {
        [Required]
        [JsonProperty("buy_order_id")]
        public string Id { get; set; }

        [Required]
        [JsonProperty("market_hash_name")]
        public string MarketHashName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [JsonProperty("suggested_price")]
        public decimal SuggestedPrice { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        [JsonProperty("created_at")]
        [JsonConverter(typeof(UnixTimestampConverter))]
        public DateTime CreatedAt { get; set; }

        [Required]
        [JsonProperty("updated_at")]
        [JsonConverter(typeof(UnixTimestampConverter))]
        public DateTime UpdatedAt { get; set; }

        // [JsonProperty("settled_with_item")]
        // public string SettledWithITEM { get; set; }

        [Required]
        [JsonProperty("place_in_queue")]
        public int PlaceInQueue { get; set; }
    }
}
