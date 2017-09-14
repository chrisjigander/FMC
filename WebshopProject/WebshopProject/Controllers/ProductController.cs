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
                    result.DropDownLinks = new DropDownLink[] {
                        new DropDownLink("Visa alla",1 ), new DropDownLink("Casualskor", 1), new DropDownLink("Businesskor", 1),
                        new DropDownLink("Outdoorskor", 1), new DropDownLink("Finskor", 1) };
                    break;
                case "2":
                    result.DropdownMenuTitle = "Varumärken";
                    result.DropDownLinks = new DropDownLink[] {
                        new DropDownLink("Visa alla", 2), new DropDownLink("Sandberg", 2), new DropDownLink("Dahlin",2),
                        new DropDownLink("Woolrish",2), new DropDownLink("Johansson",2), new DropDownLink("Jigander",2),
                        new DropDownLink("Fanny",2), new DropDownLink("Kingsley",2) };
                    break;
                case "3":
                    result.DropdownMenuTitle = "Accessoarer";
                    result.DropDownLinks = new DropDownLink[] {
                        new DropDownLink("Visa alla", 3), new DropDownLink("Bälten", 3), new DropDownLink("Slipsar", 3),
                        new DropDownLink("Näsdukar", 3) };
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

            DataManager.AddToCart(this, addProductToCart, webShopDBContext);

            return RedirectToAction(nameof(ProductItem));
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

        public IActionResult ProductOverview(char id, string link, ProductProductOverviewVM filteredVM)
        {
            ProductProductOverviewVM overviewVM;
            if (filteredVM.ShouldFilter == false)
            {
                overviewVM = webShopDBContext.GetOverview(id, link);
            }
            else
            {
                ProductProductOverviewVM filteredProductsVM = webShopDBContext.GetFiltered(filteredVM);
                webShopDBContext.SetSelectListItems(filteredProductsVM);
                overviewVM = filteredProductsVM;

            }

            return View(overviewVM);
        }

        [HttpPost]
        public IActionResult ProductOverview(ProductProductOverviewVM productVM)
        {
            return RedirectToAction(nameof(FilterOverView), productVM);
        }

        public IActionResult FilterOverView(ProductProductOverviewVM productVM)
        {
            productVM.ShouldFilter = true;
            return RedirectToAction(nameof(ProductOverview), productVM);
        }

        public string GetCartCount()
        {
            return SessionUtils.GetMultipleSessionCount(this);
        }

        public IActionResult GetCartPartial()
        {
            MyShoppingCartVM shoppingCart = SessionUtils.GetArticles(this, webShopDBContext);

            return PartialView("_MyShoppingCartPartial", shoppingCart);
        }
    }
}
