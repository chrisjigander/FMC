using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopProject.Models
{
    public class DropDownLink
    {
        public string LinkName { get; set; }
        public int CategoryId { get; set; }

        public DropDownLink(string linkName, int categoryId)
        {
            LinkName = linkName;
            CategoryId = categoryId;
        }
    }
}
