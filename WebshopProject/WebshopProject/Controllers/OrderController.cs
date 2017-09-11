using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebshopProject.Models;
using Microsoft.AspNetCore.Http;
using WebshopProject.Utils;
using WebshopProject.Models.VM;
using Microsoft.AspNetCore.Identity;
using WebshopProject.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebshopProject.Controllers
{
    public class OrderController : Controller
    {
        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;
        IdentityDbContext identityDbContext;
        WebShopDBContext webShopDBContext;


        public OrderController(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IdentityDbContext identityDbContext, WebShopDBContext webShopDBContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.identityDbContext = identityDbContext;
            this.webShopDBContext = webShopDBContext;

        }
        public IActionResult MyCart()
        {
            string[] tempArr = SessionUtils.GetArticleNumbersInCart(this);
            MyShoppingCartVM myCartVM = SessionUtils.GetArticles(this, webShopDBContext);
            if (User.Identity.IsAuthenticated)
            {
                myCartVM.IsLoggedIn = true;
            }

            return View(myCartVM);
        }

        //Olika beroende på inloggad eller inte
        public IActionResult CheckOut()
        {
            return View();
        }
        
        //Vill kunna kopplas med en emailbekräftelse
        public IActionResult ConfirmOrder()
        {
            return View();
        }

        public IActionResult Confirmed()
        {
            return View();
        }
    }
}
