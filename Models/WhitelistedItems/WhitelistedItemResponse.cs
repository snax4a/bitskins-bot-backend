using System;

namespace WebApi.Models.WhitelistedItems
{
    public class WhitelistedItemResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxQuantity { get; set; }
        public decimal PriceMultiplier { get; set; }
        public decimal Price { get; set; }
        public DateTime PriceUpdatedAt { get; set; }
        public int AccountId { get; set; }
    }
}
