using Microsoft.EntityFrameworkCore;
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
        public void AddUser(AccountRegisterVM user, string uID)
        {
            User.Add(new User
            {
                Uid = uID,
                Firstname = user.FirstName,
                Lastname = user.LastName,
                Phonenumber = user.PhoneNumber,
                Addressline = user.AddressLine,
                Zipcode = user.ZipCode,
                City = user.City,
                Email = user.Email
            });
            SaveChanges();
        }
    }
}
