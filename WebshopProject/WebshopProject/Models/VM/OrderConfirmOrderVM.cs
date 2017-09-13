using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopProject.Models.VM
{
    public class OrderConfirmOrderVM
    {
        public AccountMyProfileEditVM Account { get; set; }
        public MyShoppingCartVM ProductsToPurchase { get; set; }
    }
}
