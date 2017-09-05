using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebshopProject.Models.VM;

namespace WebshopProject.Controllers
{
    public class AccountController : Controller
    {
        // -----VIKTIG STRÄNG!!!-----//
        //signInManager.UserManager.GetUserId(HttpContext.User);


        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;
        IdentityDbContext identityDbContext;

        public AccountController(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IdentityDbContext identityDbContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.identityDbContext = identityDbContext;
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
                    return RedirectToAction("/Home/Index");  //todo Kolla om det går att byta ut den magiska strängen
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
                    return Redirect("/Home/Index"); //todo Kolla routingen och automatisk inlogging
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
                return PartialView("_LoggedInPartial");
            }
            else
            {
                return PartialView("_LogInPartial");
            }
        }
    }
}
