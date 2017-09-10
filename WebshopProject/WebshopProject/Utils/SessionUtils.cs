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
        public static string GetMultipleSessionCount(Controller controller)
        {
            int count = 0;

            for (int i = 0; i < 20; i++)
            {
                if (controller.HttpContext.Session.GetString(i.ToString()) != null)
                {
                    string[] split = controller.HttpContext.Session.GetString(i.ToString()).Split(';');
                    int nrOfSame = Convert.ToInt32(split[1]);
                    count += nrOfSame;
                }
                else
                {
                    break;

                }

            }
            return count.ToString();
        }

        public static string GetSingleSessionCount(Controller controller)
        {
            int count = 0;

            for (int i = 0; i < 20; i++)
            {
                if (controller.HttpContext.Session.GetString(i.ToString()) == null)
                {
                    break;
                }
                else
                {
                    count++;

                }


            }
            return count.ToString();
        }

        internal static MyShoppingCartPartialVM GetArticles(Controller controller, WebShopDBContext context)
        {
            //int count = Convert.ToInt32(GetSessionCount(controller));
            int count = Convert.ToInt32(GetSingleSessionCount(controller));
            int totalCost = 0;
            int totalNumberOfProducts = 0;
            MyShoppingCartPartialVM myCart = new MyShoppingCartPartialVM();
            List<ProductThumbnail> prodThumbList = new List<ProductThumbnail>();
            string currentArtNr;

            if (count != 0)
            {
                for (int i = 0; i < count; i++)
                {
                    string[] splitString = controller.HttpContext.Session.GetString(i.ToString()).Split(';');
                    currentArtNr = splitString[0];
                    Product currentProduct = context.Product.First(p => p.ProdArtNr == currentArtNr);
                    totalCost += Convert.ToInt32(currentProduct.ProdPrice) * Convert.ToInt32(splitString[1]);
                    totalNumberOfProducts += Convert.ToInt32(splitString[1]);


                    if (i < 3)
                    {
                        Model currentModel = context.Model.First(m => m.ModelId == currentProduct.ProdModelId);
                        Brand currentBrand = context.Brand.First(b => b.BrandId == currentProduct.ProdBrandId);
                        Size currentSize = context.Size.First(s => s.SizeId == currentProduct.ProdSizeId);
                        Color currentColor = context.Color.First(c => c.ColorId == currentProduct.ProdColorId);


                        ProductThumbnail currentThumbnail = new ProductThumbnail
                        {
                            Brand = currentBrand.BrandName,
                            Model = currentModel.ModelName,
                            Price = Convert.ToInt32(currentProduct.ProdPrice),
                            NumberOfSameArticle = Convert.ToInt32(splitString[1]),
                            Size = currentSize.SizeName,
                            Color = currentColor.ColorName
                            
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

        internal static int GetNumberOfSame(Controller controller, string artNr)
        {
            //int nrOfSame = 0;
            int indexOfExistingArticle = -1;

            string nrOfArticles = GetSingleSessionCount(controller);

            for (int i = 0; i < Convert.ToInt32(nrOfArticles); i++)
            {
                string[] splitString = controller.HttpContext.Session.GetString(i.ToString()).Split(';');
                if (splitString[0] == artNr)
                {
                    //nrOfSame++;
                    indexOfExistingArticle = i;

                    break;
                }
            }


            return indexOfExistingArticle;
        }
    }
}
