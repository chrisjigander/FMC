﻿using System;
using System.Collections.Generic;

namespace WebshopProject.Models.Entities
{
    public partial class Brand
    {
        public Brand()
        {
            Product = new HashSet<Product>();
        }

        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}
