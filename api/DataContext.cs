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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>().HasKey(p => p.Id);
        modelBuilder.Entity<Account>().HasKey(a => a.Id);
        modelBuilder.Entity<Cart>().HasKey(c => c.Id);

        // Configure the relationships
        modelBuilder.Entity<Account>()
            .HasOne(a => a.Cart)
            .WithOne(c => c.Account)
            .HasForeignKey<Cart>(c => c.AccountId);

        modelBuilder.Entity<Cart>()
            .HasMany(c => c.Products)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
