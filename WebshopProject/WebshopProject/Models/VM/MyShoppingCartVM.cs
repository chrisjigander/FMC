﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopProject.Models.VM
{
    public class MyShoppingCartVM
    {
        public ProductThumbnail[] Products { get; set; }
        public int TotalCost { get; set; }
        public int TotalNumberOfProducts { get; set; }
        public bool IsLoggedIn { get; set; }

    }
}
