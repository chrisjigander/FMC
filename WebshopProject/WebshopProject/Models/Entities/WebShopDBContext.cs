using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
    }
}
