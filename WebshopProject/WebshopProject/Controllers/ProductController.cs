using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebshopProject.Models.VM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebshopProject.Models.Entities;

namespace WebshopProject.Controllers
{
    public class ProductController : Controller
    {
        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;
        IdentityDbContext identityDbContext;
        WebShopDBContext webShopDBContext;

        public ProductController(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IdentityDbContext identityDbContext, WebShopDBContext webShopDBContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.identityDbContext = identityDbContext;
            this.webShopDBContext = webShopDBContext;
        }


        public IActionResult GetDropDownMenu()
        {
            return PartialView("_DropDownMenuPartial");
        }

        public IActionResult ProductItem(string id)
        {
            if (id.Length == 5 || id.Length == 7)
            {
                ProductProductItemVM productToView = webShopDBContext.GetProductToView(id);
                
                return View(productToView);

            }
            else
            {
                return View();//FEL!
            }

        }

        [HttpGet]
        public IActionResult GetPicture(string id)
        {
            return PartialView("_PictureDivPartial", new ProductPictureDivPartialVM {PictureUrl = id });
        }

        public IActionResult ReloadProductItem()
        {
            return null;
        }

        public IActionResult ProductOverview()
        {
            ProductProductOverviewVM overviewVM = webShopDBContext.GetOverview();

            return View(overviewVM);
        }
    }
}
