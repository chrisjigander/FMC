using System;
using System.Collections.Generic;

namespace WebshopProject.Models.Entities
{
    public partial class OrderArticles
    {
        public int Id { get; set; }
        public int Oid { get; set; }
        public string ArticleNumber { get; set; }
        public decimal Price { get; set; }

        public Order O { get; set; }
    }
}
