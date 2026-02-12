using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<Book> Books => Set<Book>();
        public DbSet<ShoppingCart> ShoppingCarts => Set<ShoppingCart>();
        public DbSet<CartDetail> CartDetails => Set<CartDetail>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();
        public DbSet<OrderStatus> OrderStatuses => Set<OrderStatus>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ShoppingCart>()
                .HasIndex(x => x.UserId)
                .IsUnique();

            modelBuilder.Entity<CartDetail>()
                .HasIndex(x => x.ShoppingCartId);

            modelBuilder.Entity<Order>()
                .HasIndex(x => x.UserId);

            modelBuilder.Entity<OrderDetail>()
                .HasIndex(x => x.OrderId);

            modelBuilder.Entity<Book>()
                .Property(x => x.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<CartDetail>()
                .Property(x => x.UnitPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderDetail>()
                .Property(x => x.UnitPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<CartDetail>()
                .ToTable(t => t.HasCheckConstraint("CK_CartDetail_Quantity", "[Quantity] > 0"));

            modelBuilder.Entity<CartDetail>()
                .ToTable(t => t.HasCheckConstraint("CK_CartDetail_UnitPrice", "[UnitPrice] > 0"));

            modelBuilder.Entity<OrderDetail>()
                .ToTable(t => t.HasCheckConstraint("CK_OrderDetail_Quantity", "[Quantity] > 0"));

            modelBuilder.Entity<OrderDetail>()
                .ToTable(t => t.HasCheckConstraint("CK_OrderDetail_UnitPrice", "[UnitPrice] > 0"));

            modelBuilder.Entity<IdentityUserLogin<string>>()
                .Property(x => x.LoginProvider)
                .HasMaxLength(128);

            modelBuilder.Entity<IdentityUserLogin<string>>()
                .Property(x => x.ProviderKey)
                .HasMaxLength(128);

            modelBuilder.Entity<IdentityUserToken<string>>()
                .Property(x => x.LoginProvider)
                .HasMaxLength(128);

            modelBuilder.Entity<IdentityUserToken<string>>()
                .Property(x => x.Name)
                .HasMaxLength(128);
        }
    }
}
