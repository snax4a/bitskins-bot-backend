using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.WhitelistedItems
{
    public class UpdatePricesRequest
    {
        [Required]
        public List<PriceData> PriceData { get; set; }
    }

    public class PriceData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
