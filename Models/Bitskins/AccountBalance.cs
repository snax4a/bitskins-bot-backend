using Newtonsoft.Json;

public class AccountBalance
{
    [JsonProperty("available_balance")]
    public double AvailableBalance { get; set; }
    [JsonProperty("pending_withdrawals")]
    public double PendingWithdrawals { get; set; }
    [JsonProperty("withdrawable_balance")]
    public double WithdrawableBalance { get; set; }
    [JsonProperty("couponable_balance")]
    public double CouponableBalance { get; set; }
}
