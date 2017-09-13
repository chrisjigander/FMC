using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebshopProject.Models.Entities
{
    public partial class WebShopDBContext : DbContext
    {
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Color> Color { get; set; }
        public virtual DbSet<Model> Model { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderArticles> OrderArticles { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Size> Size { get; set; }
        public virtual DbSet<User> User { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("Brand", "fmc");

                entity.Property(e => e.BrandId).HasColumnName("Brand_Id");

                entity.Property(e => e.BrandName)
                    .IsRequired()
                    .HasColumnName("Brand_Name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.ToTable("Color", "fmc");

                entity.Property(e => e.ColorId).HasColumnName("Color_Id");

                entity.Property(e => e.ColorName)
                    .IsRequired()
                    .HasColumnName("Color_Name")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Model>(entity =>
            {
                entity.ToTable("Model", "fmc");

                entity.Property(e => e.ModelId).HasColumnName("Model_Id");

                entity.Property(e => e.ModelName)
                    .IsRequired()
                    .HasColumnName("Model_Name")
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order", "fmc");

                entity.Property(e => e.DateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<OrderArticles>(entity =>
            {
                entity.ToTable("OrderArticles", "fmc");

                entity.Property(e => e.ArticleNumber)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Oid).HasColumnName("OID");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.O)
                    .WithMany(p => p.OrderArticles)
                    .HasForeignKey(d => d.Oid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderArticl__OID__0B91BA14");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProdId);

                entity.ToTable("Product", "fmc");

                entity.Property(e => e.ProdId).HasColumnName("Prod_Id");

                entity.Property(e => e.ProdArtNr)
                    .IsRequired()
                    .HasColumnName("Prod_ArtNr")
                    .HasMaxLength(30);

                entity.Property(e => e.ProdBrandId).HasColumnName("Prod_BrandId");

                entity.Property(e => e.ProdColorId).HasColumnName("Prod_ColorId");

                entity.Property(e => e.ProdDescription)
                    .HasColumnName("Prod_Description")
                    .HasMaxLength(400);

                entity.Property(e => e.ProdModelId).HasColumnName("Prod_ModelId");

                entity.Property(e => e.ProdPrice)
                    .HasColumnName("Prod_Price")
                    .HasColumnType("money");

                entity.Property(e => e.ProdQty).HasColumnName("Prod_Qty");

                entity.Property(e => e.ProdSizeId).HasColumnName("Prod_SizeId");

                entity.HasOne(d => d.ProdBrand)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProdBrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__Prod_Br__03F0984C");

                entity.HasOne(d => d.ProdColor)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProdColorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__Prod_Co__05D8E0BE");

                entity.HasOne(d => d.ProdModel)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProdModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__Prod_Mo__06CD04F7");

                entity.HasOne(d => d.ProdSize)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProdSizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__Prod_Si__04E4BC85");
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.ToTable("Size", "fmc");

                entity.Property(e => e.SizeId).HasColumnName("Size_Id");

                entity.Property(e => e.SizeName)
                    .IsRequired()
                    .HasColumnName("Size_Name")
                    .HasMaxLength(2);
            });

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

                entity.Property(e => e.Uid).HasColumnName("UID");

                entity.Property(e => e.Zipcode).HasMaxLength(5);
            });
        }
    }
}
