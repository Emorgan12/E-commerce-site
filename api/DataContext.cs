using System;
using Microsoft.EntityFrameworkCore;
using ECommerceSite;
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Order> Orders { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>().HasKey(p => p.id);
        modelBuilder.Entity<Account>().HasKey(a => a.Id);
        modelBuilder.Entity<Cart>().HasKey(c => c.Id);
        modelBuilder.Entity<Discount>().HasKey(d => d.id);
        modelBuilder.Entity<Review>().HasKey(r => r.id);
        modelBuilder.Entity<Order>().HasKey(o => o.id);

        // Configure the relationships
        modelBuilder.Entity<Account>()
        .HasOne<Cart>()
        .WithOne()
        .HasForeignKey<Account>(a => a.CartId);

        modelBuilder.Entity<Account>()
        .HasMany(a => a.orders)
        .WithOne()
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Cart>()
        .HasMany(c => c.Products)
        .WithOne()
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Product>()
        .HasMany(p => p.reviews)
        .WithOne()
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Review>()
        .HasOne<Product>()
        .WithOne()
        .HasForeignKey<Review>(r => r.reviewProductId);

        modelBuilder.Entity<Review>()
        .HasOne<Order>()
        .WithOne()
        .HasForeignKey<Review>(r => r.orderId);

        modelBuilder.Entity<Order>()
        .HasMany(o => o.products)
        .WithOne()
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Order>()
        .HasOne<Account>()
        .WithOne()
        .HasForeignKey<Order>(o => o.orderAccountId);
    }
}
