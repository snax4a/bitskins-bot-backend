namespace WebApi.Entities
{
    public class WhitelistedItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int MaxQuantity { get; set; }
        public decimal PriceMultiplier { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
