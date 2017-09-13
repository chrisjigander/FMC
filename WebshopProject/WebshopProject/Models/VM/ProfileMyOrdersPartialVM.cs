using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopProject.Models.Entities;

namespace WebshopProject.Models.VM
{
    public class ProfileMyOrdersPartialVM
    {
        public Order[] MyOrders { get; set; }
        public int[] TotalSum { get; set; }

    }
}
