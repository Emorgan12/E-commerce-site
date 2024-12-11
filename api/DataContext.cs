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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>().HasKey(p => p.id);
        modelBuilder.Entity<Account>().HasKey(a => a.Id);
        modelBuilder.Entity<Cart>().HasKey(c => c.Id);
        modelBuilder.Entity<Discount>().HasKey(d => d.id);

        // Configure the relationships
        modelBuilder.Entity<Account>()
        .HasOne<Cart>()
        .WithOne()
        .HasForeignKey<Account>(a => a.CartId);

        modelBuilder.Entity<Cart>()
        .HasMany(c => c.Products)
        .WithOne()
        .OnDelete(DeleteBehavior.Cascade);
    }
}
