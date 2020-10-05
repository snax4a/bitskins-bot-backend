using System;

namespace WebApi.Entities
{
    public class PurchasedItem
    {
        public int Id { get; set; }
        public string MarketHashName { get; set; }
        public decimal Price { get; set; }
        public decimal WikiPrice { get; set; }
        public decimal PriceMultiplier { get; set; }
        public string AppId { get; set; }
        public string ContextId { get; set; }
        public string ItemId { get; set; }
        public string AssetId { get; set; }
        public string ClassId { get; set; }
        public string InstanceId { get; set; }
        public DateTime PurchasedAt { get; set; }
        public DateTime WithdrawableAt { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
