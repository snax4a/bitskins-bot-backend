using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.WhitelistedItems
{
    public class UpdateRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Slug { get; set; }
        [Required]
        public string Image { get; set; }
        public int MaxQuantity { get; set; }
        public decimal PriceMultiplier { get; set; }
        public int AccountId { get; set; }
    }
}
