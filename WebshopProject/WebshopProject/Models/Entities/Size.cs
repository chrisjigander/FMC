using System;
using System.Collections.Generic;

namespace WebshopProject.Models.Entities
{
    public partial class Size
    {
        public Size()
        {
            Product = new HashSet<Product>();
        }

        public int SizeId { get; set; }
        public string SizeName { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}
