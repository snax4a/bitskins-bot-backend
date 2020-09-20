namespace WebApi.Entities
{
    public class Price
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal MedianPrice { get; set; }
        public decimal StandardDeviation { get; set; }
        public decimal LowestPrice { get; set; }
        public decimal HighestPrice { get; set; }
        public int Sold { get; set; }
    }
}
