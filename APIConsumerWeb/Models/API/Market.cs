using System;
using System.Collections.Generic;

namespace APIConsumerWeb.Models
{
    public class Market
    {
        public Market()
        {
            Product = new HashSet<Product>();
        }

        public int MarketId { get; set; }
        public string Name { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}
