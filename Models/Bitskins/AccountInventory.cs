using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WebApi.Models.Bitskins
{
    public class AccountInventory
    {
        [JsonProperty("app_id")]
        public string AppId { get; set; }

        [JsonProperty("pending_withdrawal_from_bitskins")]
        public PendingWithdrawal PendingWithdrawal { get; set; }
    }

    public class PendingWithdrawal
    {
        public List<BitskinsItem> Items { get; set; }
    }
}
