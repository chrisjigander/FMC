using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebshopProject.Models.VM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebshopProject.Models.Entities;
using Microsoft.AspNetCore.Http;
using WebshopProject.Models;
using WebshopProject.Utils;

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


        public IActionResult GetDropDownMenu(string id)
        {
            DropdownVM content = GetDropDownVMContent(id);
            return PartialView("_DropDownMenuPartial", content);
        }

        private DropdownVM GetDropDownVMContent(string id)
        {
            DropdownVM result = new DropdownVM();
            switch (id)
            {
                case "1":
                    result.DropdownMenuTitle = "Skor";
                    result.DropDownLinks = new string[] { "Visa alla", "Casualskor", "Businesskor", "Outdoorskor", "Finskor" };
                    break;
                case "2":
                    result.DropdownMenuTitle = "Varumärken";
                    result.DropDownLinks = new string[] { "Sandberg", "Dahlin", "Woolrish", "Johansson", "Jigander", "Fanny", "Kingsley" };
                    break;
                case "3":
                    result.DropdownMenuTitle = "Accessoarer";
                    result.DropDownLinks = new string[] { "Bälten", "Slipsar", "Näsdukar" };
                    break;

            }
            return result;
        }

        [HttpGet]
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

        [HttpPost]
        public IActionResult ProductItem(ProductProductItemVM addProductToCart)
        {
            string index = "-1";

            for (int i = 0; i < 10; i++)
            {
                if (HttpContext.Session.GetString(i.ToString()) == null)
                {
                    index = i.ToString();
                    break;
                }
            }

            if (index == "-1")
            {

            }
            else
            {
                string size = webShopDBContext.Size.First(s => s.SizeId == Convert.ToInt32(addProductToCart.SelectedSize)).SizeName;
                string artNr = $"{addProductToCart.ArticleNum}{size}";
                HttpContext.Session.SetString(index, artNr);

            }

            return RedirectToAction(nameof(ProductItem)/*, addProductToCart.ArticleNum*/);
        }

        [HttpGet]
        public IActionResult GetPicture(string id)
        {

            return PartialView("_PictureDivPartial", new ProductPictureDivPartialVM { PictureUrl = id });
        }

        public IActionResult ReloadProductItem()
        {
            return null;
        }

        public IActionResult ProductOverview(char id)
        {
            ProductProductOverviewVM overviewVM = webShopDBContext.GetOverview(id);

            return View(overviewVM);
        }

        public string GetCartCount()
        {
            return SessionUtils.GetSessionCount(this);
        }

        public IActionResult GetCartPartial()
        {
            MyShoppingCartPartialVM shoppingCart =  SessionUtils.GetArticles(this, webShopDBContext);

            return PartialView("_MyShoppingCartPartial", shoppingCart);
        }
    }
}
