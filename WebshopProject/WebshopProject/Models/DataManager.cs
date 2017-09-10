using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopProject.Controllers;
using WebshopProject.Models.Entities;
using WebshopProject.Models.VM;
using WebshopProject.Utils;

namespace WebshopProject.Models
{
    public static class DataManager
    {
        
        public static void AddToCart(ProductController controller, ProductProductItemVM prodItemVM, WebShopDBContext webShopDBContext)
        {
            string index = "-1";

            for (int i = 0; i < 20; i++)
            {
                if (controller.HttpContext.Session.GetString(i.ToString()) == null)
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
                string size = webShopDBContext.Size.First(s => s.SizeId == Convert.ToInt32(prodItemVM.SelectedSize)).SizeName;
                string artNr = $"{prodItemVM.ArticleNum}{size}";
                int numberOfSame = SessionUtils.GetNumberOfSame(controller, artNr);

                if (numberOfSame == -1)
                {
                    string sessionString = $"{artNr};1";
                    controller.HttpContext.Session.SetString(index, sessionString);

                }
                else
                {
                    string[] splitt = controller.HttpContext.Session.GetString(numberOfSame.ToString()).Split(';');
                    int numberOfArt = Convert.ToInt32(splitt[1]);
                    int newNumberOfArt = ++numberOfArt;
                    string sessionString = $"{artNr};{newNumberOfArt}";
                    controller.HttpContext.Session.SetString(numberOfSame.ToString(), sessionString);
                }
            }
        }


    }
}
