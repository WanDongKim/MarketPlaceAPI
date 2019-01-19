using System;
using System.Collections.Generic;

namespace APIConsumerWeb.Models
{
    public class Category
    {
        public Category()
        {
            Product = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}
