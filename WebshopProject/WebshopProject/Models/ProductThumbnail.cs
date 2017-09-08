using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopProject.Models
{
    public class ProductThumbnail
    {
        public string ImgPath { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Price { get; set; }
        public string ArticleNrShort { get; set; }

    }
}
