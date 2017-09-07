using System;
using System.Collections.Generic;

namespace WebshopProject.Models.Entities
{
    public partial class Color
    {
        public Color()
        {
            Product = new HashSet<Product>();
        }

        public int ColorId { get; set; }
        public string ColorName { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}
