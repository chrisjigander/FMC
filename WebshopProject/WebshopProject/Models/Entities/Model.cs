using System;
using System.Collections.Generic;

namespace WebshopProject.Models.Entities
{
    public partial class Model
    {
        public Model()
        {
            Product = new HashSet<Product>();
        }

        public int ModelId { get; set; }
        public string ModelName { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}
