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
        public DbSet<ProductCategory>? ProductCategories { get; set; }
        public DbSet<ProductSubCategory>? ProductSubCategories { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Unit>? Units { get; set; }
        public DbSet<PlaceCategory>? PlaceCategories { get; set; }
        public DbSet<Place>? Places { get; set; }
        public DbSet<ChannelCategory>? ChannelCategories { get; set; }
        public DbSet<Channel>? Channels { get; set; }
        public DbSet<Tax>? Taxes { get; set; }
        public DbSet<Ticket>? Tickets { get; set; }
        public DbSet<TicketRecipe>? TicketRecipes { get; set; }
        public DbSet<Discount>? Discounts { get; set; }
        public DbSet<Recipe>? Recipes { get; set; }
        public DbSet<RecipeProduct>? RecipeProducts { get; set; }
        public DbSet<ExtraProductCategory>? ExtraProductCategories { get; set; }
        public DbSet<ExtraProduct>? ExtraProducts { get; set; }
        public DbSet<Customer>? Customers { get; set; }
        public DbSet<TransactionCategory>? TransactionCategories { get; set; }
        public DbSet<Transaction>? Transactions { get; set; }
        public DbSet<TransactionProduct>? TransactionProducts { get; set; }
        public DbSet<TicketTax>? TicketTaxes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here

            //Property Configurations
            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasIndex(e => new { e.Name})
                .IsUnique();
            });
            
            modelBuilder.Entity<ProductSubCategory>(entity =>
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

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.HasIndex(e => new { e.Name })
                .IsUnique();
            });

            modelBuilder.Entity<PlaceCategory>(entity =>
            {
                entity.HasIndex(e => new { e.Name })
                .IsUnique();
            });

            modelBuilder.Entity<Place>(entity =>
            {
                entity.HasIndex(e => new { e.Name })
                .IsUnique();
            });
            modelBuilder.Entity<Discount>(entity =>
            {
                entity.HasIndex(e => new { e.Name })
                .IsUnique();
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.HasIndex(e => new { e.Name })
                .IsUnique();
            });

            modelBuilder.Entity<ExtraProductCategory>(entity =>
            {
                entity.HasIndex(e => new { e.Name })
                .IsUnique();
            });

            modelBuilder.Entity<ExtraProduct>(entity =>
            {
                entity.HasIndex(e => new { e.Name })
                .IsUnique();
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(e => new { e.Name })
                .IsUnique();
            });
            modelBuilder.Entity<TransactionCategory>(entity =>
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
