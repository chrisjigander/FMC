﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebshopProject.Models.Entities;
using WebshopProject.Models.VM;
using Microsoft.AspNetCore.Authorization;

namespace WebshopProject.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;
        IdentityDbContext identityDbContext;
        WebShopDBContext webShopDBContext;

        public ProfileController(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IdentityDbContext identityDbContext, WebShopDBContext webShopDBContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.identityDbContext = identityDbContext;
            this.webShopDBContext = webShopDBContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult EditProfile()
        {
            string userID = signInManager.UserManager.GetUserId(HttpContext.User);
            AccountMyProfileEditVM currentProfile = webShopDBContext.GetUserEditProfile(userID);
            return PartialView("_MyProfileEditPartial", currentProfile);
        }
        public IActionResult MyOrders()
        {
            return PartialView("", new AccountMyOrdersVM { });
        }

        [HttpPost]
        public IActionResult SaveEdit(AccountMyProfileEditVM editedUser)
        {
            string userID = signInManager.UserManager.GetUserId(HttpContext.User);

            webShopDBContext.UpdateUser(editedUser, userID);
            return RedirectToAction("Index");
        }
    }
}
