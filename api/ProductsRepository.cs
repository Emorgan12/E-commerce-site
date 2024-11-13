using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class ProductRepository : IProductsRepository
{
    private readonly DataContext _context;
    Random random = new Random();
    

    public ProductRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetProducts()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task NewProduct(string name, Uri image, string colour, float price)
    {
        var product = new Product
        {
            Id = random.Next(),
            Name = name,
            Image = image,
            Colour = colour,
            Price = price
        };

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProduct(int id)
    {
        Product product = await GetProduct(id);
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<Product> GetProduct(int id)
    {
        try
        {
            Product product = await _context.Products.FindAsync(id);
            return product;
        }
        catch
        {
            return null;
        }
    }

    public async Task UpdateProduct(int id, string name, Uri image, string colour, float price)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            throw new KeyNotFoundException();
        }
        product.Name = name;
        product.Image = image;
        product.Colour = colour;
        product.Price = price;
        _context.Products.Update(product);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new KeyNotFoundException("Product not found.");
        }
    }

    public async Task NewAccount(string username, string password, string email)
    {
        var account = new Account
        {
            Id = random.Next(),
            Username = username,
            Password = password,
            Email = email
        };
        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Account>> GetAccounts()
    {
        return await _context.Accounts.ToListAsync();
    }

    public async Task DeleteAccount(int id)
    {
        Account account = await GetAccount(id);
        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();
    }

    public async Task<Account> GetAccount(int id)
    {
        try
        {
            Account account = await _context.Accounts.FindAsync(id);
            return account;
        }
        catch
        {
            return null;
        }
    }

    public async Task UpdatePassword(int id, string password)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id);
        if (account == null)
        {
            throw new KeyNotFoundException();
        }
        account.Password = password;
        _context.Accounts.Update(account);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new KeyNotFoundException("Account not found.");
        }
    }

    public async Task UpdateEmail(int id, string email)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id);
        if (account == null)
        {
            throw new KeyNotFoundException();
        }
        account.Email = email;
        _context.Accounts.Update(account);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new KeyNotFoundException("Account not found.");
        }
    }

    public async Task NewCart(int accountId)
    {
        var cart = new Cart
        {
            Id = random.Next(),
            AccountId = accountId,
            Products = new List<Product>()
        };
        await _context.Carts.AddAsync(cart);
        await _context.SaveChangesAsync();
    }


    public async Task<List<Cart>> GetCarts()
    {
        return await _context.Carts.ToListAsync();
    }

    public async Task DeleteCart(int id)
    {
        Cart cart = await GetCart(id);
        _context.Carts.Remove(cart);
        await _context.SaveChangesAsync();
    }

    public async Task<Cart> GetCart(int id)
    {
        try
        {
            Cart cart = await _context.Carts.FindAsync(id);
            return cart;
        }
        catch
        {
            return null;
        }
    }

    public async Task UpdateCart(int id, int accountId, Product product)
    {
        var cart = await _context.Carts.FirstOrDefaultAsync(c => c.Id == id);
        if (cart == null)
        {
            throw new KeyNotFoundException();
        }
        cart.AccountId = accountId;
        cart.Products.Add(product);
        _context.Carts.Update(cart);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new KeyNotFoundException("Cart not found.");
        }
    }
}