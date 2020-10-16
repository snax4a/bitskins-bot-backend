namespace WebApi.Models.Dmarket
{
    public class Item
    {
        public string ItemId { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public string ClassId { get; set; }
        public string GameId { get; set; }
        public string GameType { get; set; }
        public bool InMarket { get; set; }
        public bool LockStatus { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Slug { get; set; }
        public string Owner { get; set; }
        public string OwnersBlockchainId { get; set; }
        public OwnerDetails OwnerDetails { get; set; }
        public string Status { get; set; }
        public int Discount { get; set; }
        public Price Price { get; set; }
        public Price InstantPrice { get; set; }
        public string InstantTargetId { get; set; }
        public Price SuggestedPrice { get; set; }
        public RecommendedPrice RecommendedPrice { get; set; }
        public Extra Extra { get; set; }
        public int CreatedAt { get; set; }
    }
}
