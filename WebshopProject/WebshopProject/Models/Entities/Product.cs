using System;
using System.Collections.Generic;

namespace WebshopProject.Models.Entities
{
    public partial class Product
    {
        public int ProdId { get; set; }
        public string ProdArtNr { get; set; }
        public int ProdQty { get; set; }
        public decimal ProdPrice { get; set; }
        public string ProdDescription { get; set; }
        public int ProdBrandId { get; set; }
        public int ProdSizeId { get; set; }
        public int ProdColorId { get; set; }
        public int ProdModelId { get; set; }

        public Brand ProdBrand { get; set; }
        public Color ProdColor { get; set; }
        public Model ProdModel { get; set; }
        public Size ProdSize { get; set; }
    }
}
