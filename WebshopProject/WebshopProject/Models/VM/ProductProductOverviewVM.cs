using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopProject.Models.VM
{
    public class ProductProductOverviewVM
    {
        public ProductThumbnail[] ProdThumbnails { get; set; }
        public SelectListItem[] SizeArray { get; set; }
        public SelectListItem[] BrandArray { get; set; }
        public SelectListItem[] PriceArray { get; set; }
        public SelectListItem[] ColorArray { get; set; }

        public string SelectedSize { get; set; }
        public string SelectedBrand { get; set; }
        public string SelectedColor { get; set; }
        public string SelectedPrice { get; set; }

        public bool ShouldFilter { get; set; }

    }
}
