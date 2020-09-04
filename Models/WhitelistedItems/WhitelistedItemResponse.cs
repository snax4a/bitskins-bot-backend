using System;

namespace WebApi.Models.WhitelistedItems
{
    public class WhitelistedItemResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int MaxQuantity { get; set; }
        public float PriceMultiplier { get; set; }
        public int AccountId { get; set; }
    }
}
