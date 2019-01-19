using System;
using System.Collections.Generic;

namespace APIConsumerWeb.Models
{
    public class Discount
    {
        public Discount()
        {
            Product = new HashSet<Product>();
        }

        public int DiscountId { get; set; }
        public float? OfferAsPercent { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}
