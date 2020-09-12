using Newtonsoft.Json;

public class Sale
{
    [JsonProperty("market_hash_name")]
    public string MarketHashName { get; set; }
    public double Price { get; set; }
    [JsonProperty("wear_value")]
    public double WearValue { get; set; }
    [JsonProperty("sold_at")]
    public double SoldAt { get; set; }
}
