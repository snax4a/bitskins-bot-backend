namespace WebApi.Models.Dmarket
{
    public class Extra
    {
        public bool Tradable { get; set; }
        public bool IsNew { get; set; }
        public string GameId { get; set; }
        public string Name { get; set; }
        public string CategoryPath { get; set; }
        public string LinkId { get; set; }
        public string Exterior { get; set; }
        public string Category { get; set; }
        public int TradeLockDuration { get; set; }
    }
}
