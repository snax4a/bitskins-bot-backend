namespace WebApi.Models.WhitelistedItems
{
    public class WhitelistedItemResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxQuantity { get; set; }
        public decimal PriceMultiplier { get; set; }
        public int AccountId { get; set; }
    }
}
