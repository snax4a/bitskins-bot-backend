using Newtonsoft.Json;

public class ItemPrice
{
    [JsonProperty("average_price")]
    public decimal Average { get; set; }

    [JsonProperty("median_price")]
    public decimal Median { get; set; }

    [JsonProperty("amount_sold")]
    public int AmountSold { get; set; }

    [JsonProperty("lowest_price")]
    public decimal Lowest { get; set; }

    [JsonProperty("highest_price")]
    public decimal Highest { get; set; }

    [JsonProperty("standard_deviation")]
    public decimal StandardDeviation { get; set; }

    public string Time { get; set; }

    public string Currency { get; set; }
}
