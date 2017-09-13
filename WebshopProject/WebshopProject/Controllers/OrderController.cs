﻿using System;
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

        [HttpPost]
        public IActionResult CheckOut(AccountMyProfileEditVM account)
        {
            return RedirectToAction(nameof(ConfirmOrder), account);
        }

        //Vill kunna kopplas med en emailbekräftelse
        [HttpGet]
        public IActionResult ConfirmOrder(AccountMyProfileEditVM account)
        {
            OrderConfirmOrderVM thingsNeededToCompletePurchase = new OrderConfirmOrderVM();
            thingsNeededToCompletePurchase.Account = account;
            thingsNeededToCompletePurchase.ProductsToPurchase = SessionUtils.GetArticles(this, webShopDBContext);
            return View(thingsNeededToCompletePurchase);
        }

        [HttpPost]
        public IActionResult Confirmed()
        {
            return View();
        }

        public IActionResult EditProduct(string artNr, string size, int plusOrMinus)
        {
 
            SessionUtils.EditProduct(this, artNr, size, plusOrMinus);
            return RedirectToAction(nameof(MyCart));
        }

        public IActionResult RemoveProduct(string artNr, string size)
        {
            SessionUtils.RemoveProduct(this, artNr, size);
            return RedirectToAction(nameof(MyCart));
        }
    }
}
