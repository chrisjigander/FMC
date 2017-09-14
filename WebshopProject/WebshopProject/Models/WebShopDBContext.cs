﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopProject.Models.VM;
using WebshopProject.Utils;

namespace WebshopProject.Models.Entities
{
    public partial class WebShopDBContext
    {

        public WebShopDBContext(DbContextOptions<WebShopDBContext> options) : base(options)
        {

        }
        public void AddUser(JObject user, string uID)
        {
            string test = (string)user["FirstName"];
            User.Add(new User
            {
                Uid = uID,
                Firstname = (string)user["FirstName"],
                Lastname = (string)user["LastName"],
                Phonenumber = (string)user["PhoneNumber"],
                Addressline = (string)user["AddressLine"],
                Zipcode = (string)user["ZipCode"],
                City = (string)user["City"],
                Email = (string)user["Email"]
            });
            SaveChanges();

            User tempUser = User.First(u => u.Uid == uID);
            EmailUtils.SendRegistrationConfEmail(tempUser);
        }
        internal AccountMyProfileVM GetUserProfile(string userID)
        {

            User currentUser = this.User.First(u => u.Uid == userID);
            return new AccountMyProfileVM
            {
                FirstName = currentUser.Firstname,
                LastName = currentUser.Lastname,
                AddressLine = currentUser.Addressline,
                ZipCode = currentUser.Zipcode,
                City = currentUser.City,
                Email = currentUser.Email,
                PhoneNumber = currentUser.Phonenumber
            };
        }

        internal AccountMyProfileEditVM GetUserEditProfile(string userID)
        {
            User currentUser = this.User.First(u => u.Uid == userID);
            return new AccountMyProfileEditVM
            {
                FirstName = currentUser.Firstname,
                LastName = currentUser.Lastname,
                AddressLine = currentUser.Addressline,
                ZipCode = currentUser.Zipcode,
                City = currentUser.City,
                Email = currentUser.Email,
                PhoneNumber = currentUser.Phonenumber
            };
        }

        internal void UpdateUser(AccountMyProfileEditVM editedUser, string userID)
        {

            User currentUser = this.User.First(u => u.Uid == userID);
            currentUser.Firstname = editedUser.FirstName;
            currentUser.Lastname = editedUser.LastName;
            currentUser.Addressline = editedUser.AddressLine;
            currentUser.Zipcode = editedUser.ZipCode;
            currentUser.City = editedUser.City;
            currentUser.Phonenumber = editedUser.PhoneNumber;
            currentUser.Email = editedUser.Email;
            SaveChanges();
        }


        internal ProductProductItemVM GetProductToView(string articleNum)
        {
            string artNrShort;
            int specificColor = int.Parse(articleNum[4].ToString());

            if (articleNum.Length == 5)
            {
                Product product = this.Product.Where(p => p.ProdArtNr.StartsWith(articleNum)).Where(p => p.ProdQty > 0).First();

                artNrShort = articleNum;
            }
            else
            {
                artNrShort = articleNum.Remove(articleNum.Length - 2);
            }

            Product currentProduct = this.Product.First(p => p.ProdArtNr.Remove(p.ProdArtNr.Length - 2) == artNrShort);

            var colorArray = GetAllColors(artNrShort.Remove(artNrShort.Length - 1));
            var sizeArray = GetAllSizes(currentProduct.ProdBrandId, currentProduct.ProdModelId, colorArray.First(c => c.Value == specificColor.ToString()).Text);
            var imageArray = GetAllImages(artNrShort);

            var prodModel = Model.First(m => m.ModelId == currentProduct.ProdModelId).ModelName;
            var prodBrand = Brand.First(b => b.BrandId == currentProduct.ProdBrandId).BrandName;


            var ret = new ProductProductItemVM
            {
                ArticleNum = artNrShort,
                ColorArray = colorArray,
                Description = currentProduct.ProdDescription,
                ImageArray = imageArray,
                Price = Convert.ToInt32(currentProduct.ProdPrice),
                Model = prodModel,
                Brand = prodBrand,
                SizeArray = sizeArray,
                SelectedColor = specificColor.ToString()
            };
            return ret;
        }

        private string[] GetAllImages(string articleNum)
        {

            List<string> imageFileList = new List<string>();
            string shortArticleNum = articleNum;

            if (articleNum[0] == '1')
            {
                for (int i = 1; i < 5; i++)
                {
                    imageFileList.Add(shortArticleNum + "_" + i + ".JPG");

                }

            }
            else
            {
                imageFileList.Add(shortArticleNum + "_" + 1 + ".JPG");

            }

            return imageFileList.ToArray();

        }

        private SelectListItem[] GetAllSizes(int brand, int model, string color)
        {
            int colorId = Color.First(c => c.ColorName == color).ColorId;
            List<SelectListItem> sizeList = new List<SelectListItem>();
            Product[] productArray = Product.Where(p => p.ProdBrandId == brand).Where(p => p.ProdModelId == model).ToArray();

            foreach (var product in productArray)
            {
                if (product.ProdQty > 0 && product.ProdColorId == colorId)
                {
                    sizeList.Add(new SelectListItem { Text = Size.First(s => s.SizeId == product.ProdSizeId).SizeName, Value = product.ProdSizeId.ToString() });
                }
            }

            // sizeList.OrderBy(s => Convert.ToInt32(s.Value));
            return sizeList.OrderBy(s => Convert.ToInt32(s.Value)).ToArray();
        }

        private SelectListItem[] GetAllColors(string artNr)
        {
            List<SelectListItem> colorList = new List<SelectListItem>();
            Product[] productArray = Product.Where(p => p.ProdArtNr.Remove(p.ProdArtNr.Length - 3) == artNr).ToArray();
            List<int> checkedProductList = new List<int>();

            foreach (var product in productArray)
            {
                if (product.ProdQty > 0)
                {
                    if (checkedProductList.Count > 0)
                    {
                        int iterationCount = 0;

                        foreach (var item in checkedProductList)
                        {
                            if (item == product.ProdColorId)
                            {
                                iterationCount++;
                            }
                        }
                        if (iterationCount == 0)
                        {
                            colorList.Add(new SelectListItem { Text = Color.First(s => s.ColorId == product.ProdColorId).ColorName, Value = product.ProdColorId.ToString() });
                            checkedProductList.Add(product.ProdColorId);
                        }
                    }
                    else
                    {
                        colorList.Add(new SelectListItem { Text = Color.First(s => s.ColorId == product.ProdColorId).ColorName, Value = product.ProdColorId.ToString() });
                        checkedProductList.Add(product.ProdColorId);
                    }
                }
            }

            return colorList.ToArray();
        }

        internal ProductProductOverviewVM GetOverview(char id, string link)
        {
            List<ProductThumbnail> thumbnailList = new List<ProductThumbnail>();
            List<Product> listOfProductsToReturn = new List<Entities.Product>();
            List<Product> allProductsToFilter = new List<Entities.Product>();
            allProductsToFilter = Product.ToList();

            //check if id is not null
            if (int.TryParse(id.ToString(), out int result))
            {
                if (link == "Visa alla")
                {
                    if (id == '3')
                    {
                        allProductsToFilter = allProductsToFilter.Where(p => p.ProdQty > 0).ToList();

                    }
                    else
                    {
                        allProductsToFilter = allProductsToFilter.Where(p => p.ProdArtNr.StartsWith(id)).ToList();

                    }
                }
                else if (id == '1' && link != null)
                {
                    string[] arr = new string[3];
                    arr[0] = "10013";
                    arr[1] = "10082";
                    arr[2] = "10083";

                    if (link == "Businesskor")
                    {
                        arr[0] = "10043";
                        arr[1] = "10073";
                        arr[2] = "10022";
                    }

                    else if (link == "Outdoorskor")
                    {
                        arr[0] = "10052";
                        arr[1] = "10066";
                        arr[2] = "10032";
                    }

                    else if (link == "Finskor")
                    {
                        arr[0] = "10106";
                        arr[1] = "10092";
                        arr[2] = "10093";
                    }
                    List<Product> tempList = new List<Product>();

                    for (int i = 0; i < arr.Length; i++)
                    {
                        tempList.Add(allProductsToFilter.First(p => p.ProdArtNr.Contains(arr[i])));
                    }
                    allProductsToFilter = tempList;
                }
                else if (id == '2')
                {
                    int prodbrand = 6;

                    if (link == "Bälten")
                    {
                        prodbrand = 7;
                    }
                    else if (link == "Näsdukar")
                    {
                        prodbrand = 8;
                    }
                    allProductsToFilter = allProductsToFilter.Where(p => p.ProdArtNr.StartsWith(id)).Where(p => p.ProdBrandId == prodbrand).ToList();

                }
                else if (id == '3')
                {
                    int brandID = Brand.First(b => b.BrandName == link).BrandId;
                    allProductsToFilter = allProductsToFilter.Where(p => p.ProdBrandId == brandID).ToList();
                }
            }
            else if (link != null)
            {
                int brandID = Brand.First(b => b.BrandName == link).BrandId;
                allProductsToFilter = allProductsToFilter.Where(p => p.ProdBrandId == brandID).ToList();
            }

            foreach (var item in allProductsToFilter)
            {
                if (listOfProductsToReturn.Count == 0)
                {
                    listOfProductsToReturn.Add(item);
                }
                else
                {
                    bool productFound = false;

                    foreach (var product in listOfProductsToReturn)
                    {
                        if (product.ProdArtNr.Substring(0, 5) == item.ProdArtNr.Substring(0, 5))
                        {
                            productFound = true;
                            break;
                        }

                    }
                    if (!productFound)
                    {
                        listOfProductsToReturn.Add(item);

                    }
                }
            }

            foreach (var item in listOfProductsToReturn)
            {
                string currBrandName = Brand.FirstOrDefault(b => b.BrandId == item.ProdBrandId).BrandName;
                string currModelName = Model.FirstOrDefault(m => m.ModelId == item.ProdModelId).ModelName;
                thumbnailList.Add(new ProductThumbnail
                {
                    ImgPath = $"{item.ProdArtNr.Remove(item.ProdArtNr.Length - 2)}_1.jpg",
                    ArticleNrShort = item.ProdArtNr.Substring(0, 5),
                    Brand = currBrandName,
                    Model = currModelName,
                    Price = Convert.ToInt32(item.ProdPrice)

                });

            }
            return new ProductProductOverviewVM { ProdThumbnails = thumbnailList.ToArray() };
        }

        internal ProductProductItemVM GetAccessoriesView(string id)
        {
            return null;
        }

        internal void AddOrder(int customerId, MyShoppingCartVM myCart, WebShopDBContext context)
        {
            DateTime timeStamp = DateTime.Now;
            Order.Add(new Order { DateTime = timeStamp, CustomerId = customerId });
            SaveChanges();
            int OID = Order.First(o => o.DateTime == timeStamp).Id;

            foreach (var article in myCart.Products)
            {
                OrderArticles.Add(new OrderArticles
                {
                    Oid = OID,
                    ArticleNumber = $"{article.ArticleNrShort}{article.Size}",
                    Price = article.Price
                });
                SaveChanges();
            };
            User myUser = context.User.First(c => c.Id == customerId);
            EmailUtils.SendOrderConfEmail(OID, context, myUser);
        }

        internal ProfileMyOrdersPartialVM GetMyOrders(int customerID)
        {
            ProfileMyOrdersPartialVM profileMyOrders = new ProfileMyOrdersPartialVM();

            profileMyOrders.MyOrders = Order.Where(o => o.CustomerId == customerID).ToArray();

            profileMyOrders.TotalSum = new int[profileMyOrders.MyOrders.Length];

            for (int i = 0; i < profileMyOrders.MyOrders.Length; i++)
            {
                int price = 0;

                OrderArticles[] orderArticles = OrderArticles.Where(o => o.Oid == profileMyOrders.MyOrders[i].Id).ToArray();

                foreach (var item in orderArticles)
                {
                    price += Convert.ToInt32(item.Price);
                }

                profileMyOrders.TotalSum[i] = price;
            }

            return profileMyOrders;
        }

        internal ProfileMySpecificOrderPartialVM GetSpecificOrder(int id)
        {
            ProfileMySpecificOrderPartialVM myOrder = new ProfileMySpecificOrderPartialVM();
            OrderArticles[] orderArticles = OrderArticles.Where(o => o.Oid == id).ToArray();
            List<ProductThumbnail> prodThumbList = new List<ProductThumbnail>();

            myOrder.OrderArticles = new ProductThumbnail[orderArticles.Length];

            foreach (var item in orderArticles)
            {
                bool IsExisting = false;
                if (prodThumbList.Count > 0)
                {

                    foreach (var prod in prodThumbList)
                    {
                        if (item.ArticleNumber == $"{prod.ArticleNrShort}{prod.Size}")
                        {
                            int articleCount = prod.NumberOfSameArticle;
                            articleCount++;

                            prod.NumberOfSameArticle = articleCount;
                            IsExisting = true;

                        }

                    }

                }
                if (!IsExisting)
                {

                    Product currentProduct = Product.First(p => p.ProdArtNr == item.ArticleNumber);
                    Brand currentBrand = Brand.First(b => b.BrandId == currentProduct.ProdBrandId);
                    Model currentModel = Model.First(m => m.ModelId == currentProduct.ProdModelId);
                    Size currentSize = Size.First(s => s.SizeId == currentProduct.ProdSizeId);
                    Color currentColor = Color.First(c => c.ColorId == currentProduct.ProdColorId);


                    ProductThumbnail currentThumbnail = new ProductThumbnail
                    {
                        Brand = currentBrand.BrandName,
                        Model = currentModel.ModelName,
                        Price = Convert.ToInt32(currentProduct.ProdPrice),
                        NumberOfSameArticle = 1,
                        Size = currentSize.SizeName,
                        Color = currentColor.ColorName,
                        ImgPath = $"{currentProduct.ProdArtNr.Remove(currentProduct.ProdArtNr.Length - 2)}_1.jpg",
                        ArticleNrShort = currentProduct.ProdArtNr.Substring(0, 5)

                    };
                    prodThumbList.Add(currentThumbnail);


                }
            }

            myOrder.OrderArticles = prodThumbList.ToArray();
            return myOrder;
            
        }
    }
}
