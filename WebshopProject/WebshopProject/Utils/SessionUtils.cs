using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopProject.Controllers;
using WebshopProject.Models;
using WebshopProject.Models.Entities;
using WebshopProject.Models.VM;

namespace WebshopProject.Utils
{
    public static class SessionUtils
    {
        public static string GetSessionCount(Controller controller)
        {
            string count = "-1";

            for (int i = 0; i < 20; i++)
            {
                if (controller.HttpContext.Session.GetString(i.ToString()) == null)
                {
                    count = i.ToString();
                    break;
                }

            }
            return count;
        }

        internal static MyShoppingCartPartialVM GetArticles(Controller controller, WebShopDBContext context)
        {
            int count = Convert.ToInt32(GetSessionCount(controller));
            int totalCost = 0;
            int totalNumberOfProducts = 0;
            MyShoppingCartPartialVM myCart = new MyShoppingCartPartialVM();
            List<ProductThumbnail> prodThumbList = new List<ProductThumbnail>();
            string currentArtNr;

            if (count != 0)
            {
                for (int i = 0; i < count; i++)
                {
                    currentArtNr = (controller.HttpContext.Session.GetString(i.ToString()));
                    Product currentProduct = context.Product.First(p => p.ProdArtNr == currentArtNr);
                    totalCost += Convert.ToInt32(currentProduct.ProdPrice);
                    totalNumberOfProducts++;

                    if (i < 3)
                    {
                        Model currentModel = context.Model.First(m => m.ModelId == currentProduct.ProdModelId);
                        Brand currentBrand = context.Brand.First(b => b.BrandId == currentProduct.ProdBrandId);


                        ProductThumbnail currentThumbnail = new ProductThumbnail
                        {
                            Brand = currentBrand.BrandName,
                            Model = currentModel.ModelName,
                            Price = Convert.ToInt32(currentProduct.ProdPrice)
                        };
                        prodThumbList.Add(currentThumbnail);
                    }

                }
                myCart.Products = prodThumbList.ToArray();

            }

            myCart.TotalNumberOfProducts = totalNumberOfProducts;
            myCart.TotalCost = totalCost;
            return myCart;


        }
    }
}
