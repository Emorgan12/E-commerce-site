using System;
using Microsoft.EntityFrameworkCore;

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
        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Image).IsRequired();
            entity.Property(e => e.Colour).IsRequired();
            entity.Property(e => e.Price).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(350).IsRequired();
        });
        modelBuilder.Entity<Account>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.Username).IsRequired();
            entity.HasOne(a => a.Cart)
                .WithOne(c => c.Account)
                .HasForeignKey<Cart>(c => c.AccountId);
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.AccountId).IsRequired();
            entity.HasOne(c => c.Account)
                .WithOne(a => a.Cart)
                .HasForeignKey<Cart>(c => c.AccountId);
            entity.HasMany(e => e.Products);
        });
    }
}
