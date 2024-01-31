using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Dropify.Models
{
    public partial class prn211_dropshippingContext : DbContext
    {
        public prn211_dropshippingContext()
        {
        }

        public prn211_dropshippingContext(DbContextOptions<prn211_dropshippingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<News> News { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductDetail> ProductDetails { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserAddress> UserAddresses { get; set; } = null!;
        public virtual DbSet<UserDetail> UserDetails { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration configRoot = builder.Build();
            optionsBuilder.UseSqlServer(configRoot.GetConnectionString("prn211_dropshipping"));
        }

//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//                optionsBuilder.UseSqlServer("Server=DESKTOP-IRABI1B\\MSSQLSERVER01; database=prn211_dropshipping;uid=sa;pwd=123;TrustServerCertificate=true");
//           }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName).HasMaxLength(100);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.Property(e => e.NewsId).HasColumnName("NewsID");

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ImgUrl)
                    .HasMaxLength(255)
                    .HasColumnName("ImgURL");

                entity.Property(e => e.NewsContents).HasColumnType("text");

                entity.Property(e => e.NewsType).HasMaxLength(50);

                entity.Property(e => e.Statis).HasMaxLength(50);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__News__ProductId__5165187F");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderedDate).HasColumnType("datetime");

                entity.Property(e => e.OrderedPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ShipStatus).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK__Orders__AddressI__4AB81AF0");

                entity.HasOne(d => d.Ud)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Udid)
                    .HasConstraintName("FK__Orders__Udid__49C3F6B7");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.Property(e => e.Attribute).HasColumnType("text");

                entity.Property(e => e.OrderedPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(150);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderDeta__Order__4E88ABD4");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__OrderDeta__Produ__4D94879B");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.OriginalPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SellOutPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Shipdate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Products__Catego__3C69FB99");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK__Products__Suppli__3B75D760");
            });

            modelBuilder.Entity<ProductDetail>(entity =>
            {
                entity.Property(e => e.Attribute).HasColumnType("text");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(150);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__ProductDe__Produ__3F466844");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.Property(e => e.ContactEmail).HasMaxLength(255);

                entity.Property(e => e.ContactNumber).HasMaxLength(20);

                entity.Property(e => e.CooperateDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.SupplierName).HasMaxLength(100);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("PK__Users__C5B69A4A817DEA74");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Pword).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<UserAddress>(entity =>
            {
                entity.HasKey(e => e.AddressId)
                    .HasName("PK__UserAddr__091C2AFBB07A518D");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Ud)
                    .WithMany(p => p.UserAddresses)
                    .HasForeignKey(d => d.Udid)
                    .HasConstraintName("FK__UserAddres__Udid__46E78A0C");
            });

            modelBuilder.Entity<UserDetail>(entity =>
            {
                entity.HasKey(e => e.Udid)
                    .HasName("PK__UserDeta__5E657BBFCA5C763E");

                entity.Property(e => e.Dob).HasColumnType("datetime");

                entity.Property(e => e.ImgUrl)
                    .HasMaxLength(255)
                    .HasColumnName("ImgURL");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.Property(e => e.Sex).HasMaxLength(10);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.UidNavigation)
                    .WithMany(p => p.UserDetails)
                    .HasForeignKey(d => d.Uid)
                    .HasConstraintName("FK__UserDetails__Uid__440B1D61");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
