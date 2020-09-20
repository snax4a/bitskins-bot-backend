using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.WhitelistedItems
{
    public class CreateRequest
    {
        [Required]
        public string Name { get; set; }
        public int MaxQuantity { get; set; }
        public decimal PriceMultiplier { get; set; }
        public int AccountId { get; set; }
    }
}
