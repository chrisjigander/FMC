using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopProject.Models.VM;

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

        internal ProductProductOverviewVM GetOverview(char id)
        {
            List<ProductThumbnail> thumbnailList = new List<ProductThumbnail>();
            List<Product> listOfRelevantProducts = new List<Entities.Product>();

            foreach (var item in Product.Where(p => p.ProdArtNr.StartsWith(id)))
            {
                if (listOfRelevantProducts.Count == 0)
                {
                    listOfRelevantProducts.Add(item);
                }
                else
                {
                    bool productFound = false;

                    foreach (var product in listOfRelevantProducts)
                    {
                        if (product.ProdArtNr.Substring(0, 5) == item.ProdArtNr.Substring(0, 5))
                        {
                            productFound = true;
                            break;
                        }

                    }
                    if (!productFound)
                    {
                        listOfRelevantProducts.Add(item);

                    }
                }
            }

            foreach (var item in listOfRelevantProducts)
            {
                string brandName = Brand.FirstOrDefault(b => b.BrandId == item.ProdBrandId).BrandName;
                string modelName = Model.FirstOrDefault(m => m.ModelId == item.ProdModelId).ModelName;
                thumbnailList.Add(new ProductThumbnail
                {
                    ImgPath = $"{item.ProdArtNr.Remove(item.ProdArtNr.Length - 2)}_1.jpg",
                    ArticleNrShort = item.ProdArtNr.Substring(0, 5),
                    Brand = brandName,
                    Model = modelName,
                    Price = Convert.ToInt32(item.ProdPrice)
                
                });

            }
            return new ProductProductOverviewVM { ProdThumbnails = thumbnailList.ToArray() };
        }

        internal ProductProductItemVM GetAccessoriesView(string id)
        {
            return null;
        }
    }
}
