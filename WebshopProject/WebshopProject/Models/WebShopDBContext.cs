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
        public virtual DbSet<User> User { get; set; }

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
    }
}
