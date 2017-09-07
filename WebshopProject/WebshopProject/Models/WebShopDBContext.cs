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
            Product currentProduct = this.Product.First(p => p.ProdArtNr == articleNum);
            var colorArray = GetAllColors(currentProduct.ProdBrandId, currentProduct.ProdModelId);
            var sizeArray = GetAllSizes(currentProduct.ProdBrandId, currentProduct.ProdModelId);
            var imageArray = GetAllImages(articleNum);

            var prodModel = Model.First(m => m.ModelId == currentProduct.ProdModelId).ModelName;
            var prodBrand = Brand.First(b => b.BrandId == currentProduct.ProdBrandId).BrandName;


            return new ProductProductItemVM
            {
                ArticleNum = currentProduct.ProdArtNr,
                ColorArray = colorArray,
                Description = currentProduct.ProdDescription,
                ImageArray = imageArray,
                Price = currentProduct.ProdPrice,
                Model = prodModel,
                Brand = prodBrand,
                SizeArray = sizeArray,
                CurrentDisplayedPic = 0
            };

        }

        private string[] GetAllImages(string articleNum)
        {
            List<string> imageFileList = new List<string>();
            string shortArticleNum = articleNum;
            shortArticleNum = shortArticleNum.Remove(articleNum.Length - 2); //Plocka ut

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

        private string[] GetAllSizes(int brand, int model)
        {
            List<string> sizeList = new List<string>();
            Product[] productArray = Product.Where(p => p.ProdBrandId == brand).Where(p => p.ProdModelId == model).ToArray();

            foreach (var product in productArray)
            {
                if (product.ProdQty > 0)
                {
                    sizeList.Add(Size.First(s => s.SizeId == product.ProdSizeId).SizeName);
                }
            }

            return sizeList.ToArray();
        }

        private string[] GetAllColors(int brand, int model)
        {
            List<string> colorList = new List<string>();
            Product[] productArray = Product.Where(p => p.ProdBrandId == brand).Where(p => p.ProdModelId == model).ToArray();

            foreach (var product in productArray)
            {
                if (product.ProdQty > 0)
                {
                    colorList.Add(Color.First(s => s.ColorId == product.ProdColorId).ColorName);
                }
            }

            return colorList.ToArray();
        }
    }
}
