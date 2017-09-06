using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebshopProject.Models.VM;
using WebshopProject.Models.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebshopProject.Controllers
{
    public class AccountController : Controller
    {
        // -----VIKTIG STRÄNG!!!-----//
        //signInManager.UserManager.GetUserId(HttpContext.User);


        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;
        IdentityDbContext identityDbContext;
        WebShopDBContext webShopDBContext;

        public AccountController(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IdentityDbContext identityDbContext, WebShopDBContext webShopDBContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.identityDbContext = identityDbContext;
            this.webShopDBContext = webShopDBContext;
        }

        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.UserName, model.PassWord, false, false);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError(nameof(AccountLoginVM.UserName), result.ToString());
                }
                else
                {
                    return Redirect("/Home/Index");  //todo Kolla om det går att byta ut den magiska strängen
                }

            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            await identityDbContext.Database.EnsureCreatedAsync();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await userManager.CreateAsync(new IdentityUser(model.UserName), (model.PassWord));

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("UserName", result.Errors.First().Description);
                }
                else
                {
                    var res2 = await signInManager.PasswordSignInAsync(model.UserName, model.PassWord, false, false);

                    TempData["User"] = JsonConvert.SerializeObject(model);
                    return RedirectToAction(nameof(AddUser));
                }

            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();

            return Redirect("/Home/Index"); //todo Kolla routingen
        }

        public IActionResult GetLoginForm()
        {
            if (User.Identity.IsAuthenticated)
            {
                AccountLoggedInVM VM = new AccountLoggedInVM { UserName = User.Identity.Name };
                return PartialView("_LoggedInPartial", VM);
            }
            else
            {
                return PartialView("_LogInPartial");
            }
        }
        public IActionResult AddUser()
        {
            string userID = signInManager.UserManager.GetUserId(HttpContext.User);
            string userToString = (string)TempData["User"];
            JObject userToJobject = JObject.Parse(userToString);
            
            webShopDBContext.AddUser(userToJobject, userID);
            return Redirect("/Home/Index");
        }
        public IActionResult MyProfile()
        {
            string userID = signInManager.UserManager.GetUserId(HttpContext.User);
            AccountMyProfileVM currentProfile = webShopDBContext.GetUserProfile(userID);
            return PartialView("_MyProfilePartial", currentProfile);
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

            return Redirect("/Home/Index");
        }
    }
}
