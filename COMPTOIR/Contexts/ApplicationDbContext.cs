using COMPTOIR.Models;
using COMPTOIR.Models.AppModels;
using COMPTOIR.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace COMPTOIR.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Unit>? Units { get; set; } 
        public DbSet<Tax>? Taxes { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<PaymentChannel> PaymentChannels { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Terminal> Terminals { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderTax> OrderTaxes { get; set; }
        public DbSet<Discount> Discounts  { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here
            modelBuilder.Entity<Unit>(entity =>
            {
                entity.HasIndex(e => new { e.Name })
                .IsUnique();
            });
            modelBuilder.Entity<Tax>(entity =>
            {
                entity.HasIndex(e => new { e.Name })
                .IsUnique();
            });
            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.HasIndex(e => new { e.Name })
                .IsUnique();
            });
            modelBuilder.Entity<PaymentChannel>(entity =>
            {
                entity.HasIndex(e => new { e.Name })
                .IsUnique();
            });
            modelBuilder.Entity<Place>(entity =>
            {
                entity.HasIndex(e => new { e.Name })
                .IsUnique();
            });
            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasIndex(e => new { e.Name })
                .IsUnique();
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasIndex(e => new { e.Name })
                .IsUnique();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => new { e.Name })
                .IsUnique();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => new { e.Code })
                .IsUnique();
            });

            modelBuilder.Entity<Terminal>(entity =>
            {
                entity.HasIndex(e => new { e.Name })
                .IsUnique();
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.HasIndex(e => new { e.Name })
                .IsUnique();
            });

            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.HasIndex(e => new { e.Name })
                .IsUnique();
            });
            //modelBuilder.Entity<PlaceProduct>(builder =>
            //{
            //     builder.HasNoKey();
            //     builder.ToTable("PlaceProduct");
            // });

            base.OnModelCreating(modelBuilder);
        }
    }
}
