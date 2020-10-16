namespace WebApi.Models.Dmarket
{
    public class Target
    {
        public string Id { get; set; }
        public TargetBody Body { get; set; }
    }

    public class TargetBody
    {
        public int Amount { get; set; }
        public string GameId { get; set; }
        public TargetPrice Price { get; set; }
        public TargetAttributes Attributes { get; set; }
    }

    public class TargetPrice
    {
        public string Amount { get; set; }
        public string Currency { get; set; }
    }

    public class TargetAttributes
    {
        public string Category { get; set; }
        public string Exterior { get; set; }
        public string GameId { get; set; }
        public string CategoryPath { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public TargetPrice OwnerGets { get; set; }
    }
}
