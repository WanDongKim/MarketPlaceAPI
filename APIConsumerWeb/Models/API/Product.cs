using System;
using System.Collections.Generic;

namespace APIConsumerWeb.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int CategoryId { get; set; }
        public int MarketId { get; set; }
        public int DiscountId { get; set; }
        public string PictureId { get; set; }

        public Category Category { get; set; }
        public Discount Discount { get; set; }
        public Market Market { get; set; }
    }
}
