using System.Collections.Generic;

namespace WebApi.Models.Dmarket
{
    public class ItemsResponse
    {
        public List<Item> Objects { get; set; }
        public ObjectTotals Total { get; set; }
    }
}
