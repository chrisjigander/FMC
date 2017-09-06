using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebshopProject.Models.VM;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using System.Linq;

namespace WebshopProject.Models.Entities
{
    public partial class WebShopDBContext : DbContext
    {
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "fmc");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__User__A9D105342C9DAAB3")
                    .IsUnique();

                entity.HasIndex(e => e.Uid)
                    .HasName("UQ__User__C5B19603C4A358B3")
                    .IsUnique();

                entity.Property(e => e.Addressline).HasMaxLength(40);

                entity.Property(e => e.City).HasMaxLength(40);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Phonenumber).HasMaxLength(12);

                entity.Property(e => e.Uid)
                    .IsRequired()
                    .HasColumnName("UID");

                entity.Property(e => e.Zipcode).HasMaxLength(5);
            });
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
    }
}
