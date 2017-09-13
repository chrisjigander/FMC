using System;
using System.Collections.Generic;

namespace WebshopProject.Models.Entities
{
    public partial class Order
    {
        public Order()
        {
            OrderArticles = new HashSet<OrderArticles>();
        }

        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int CustomerId { get; set; }

        public ICollection<OrderArticles> OrderArticles { get; set; }
    }
}
