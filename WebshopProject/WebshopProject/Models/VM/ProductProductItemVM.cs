using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopProject.Models.VM
{
    public class ProductProductItemVM
    {
        public string ArticleNum { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string[] ImageArray { get; set; }
        public string[] ColorArray { get; set; }
        public string[] SizeArray { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }

    }
}
