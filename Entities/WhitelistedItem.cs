using System;
using System.Collections.Generic;

namespace WebApi.Entities
{
    public class WhitelistedItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int MaxQuantity { get; set; }
        public float PriceMultiplier { get; set; }
    }
}