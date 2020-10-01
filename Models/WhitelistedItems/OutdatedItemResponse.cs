using System;

namespace WebApi.Models.WhitelistedItems
{
    public class OutdatedItemResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime PriceUpdatedAt { get; set; }
    }
}
