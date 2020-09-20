using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebApi.Models.Bitskins
{
    public class ProcessItemsRequest
    {
        [Required]
        public List<BitskinsItem> Items { get; set; }
    }
}
