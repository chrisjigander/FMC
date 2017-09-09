using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopProject.Models.VM
{
    public class ProductProductItemVM
    {
        public string ArticleNum { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string[] ImageArray { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string ArticleCount { get; set; }



        public SelectListItem[] ColorArray { get; set; }

        [Display(Name = "Färg: ")]
        public string SelectedColor { get; set; }



        public SelectListItem[] SizeArray { get; set; }

        [Display(Name = "Storlek: ")]
        public string SelectedSize { get; set; }

    }
}
