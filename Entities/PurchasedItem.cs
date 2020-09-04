using System;
using System.Collections.Generic;

namespace WebApi.Entities
{
    public class PurchasedItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public float Price { get; set; }
        public DateTime Date { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
