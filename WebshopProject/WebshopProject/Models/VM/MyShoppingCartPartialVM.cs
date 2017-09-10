using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopProject.Models.VM
{
    public class MyShoppingCartPartialVM
    {
        public ProductThumbnail[] Products { get; set; }
        public int TotalCost { get; set; }
        public int TotalNumberOfProducts { get; set; }

    }
}
