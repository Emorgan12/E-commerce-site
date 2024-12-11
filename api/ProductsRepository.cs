using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using ECommerceSite;
using Microsoft.Extensions.Logging;


public class ProductRepository : IProductsRepository
{
    private readonly DataContext _context;
    private readonly ILogger<ProductRepository> _logger;
    Random random = new Random();

    public ProductRepository(DataContext context, ILogger<ProductRepository> logger)
    {
        _context = context;
        _logger = logger;
    }


    public async Task<List<Product>> GetProducts()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task NewProduct(string Pname, string Pimage, string Pcolour, float Pprice, string Pdescription)
    {
        var product = new Product
        {
            name = Pname,
            image = Pimage ?? throw new ArgumentNullException(nameof(Pimage), "Image cannot be null."),
            colour = Pcolour,
            price = Pprice,
            description = Pdescription
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

    public async Task UpdateProduct(int id, string name, string image, string colour, float price)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.id == id);
        if (product == null)
        {
            throw new KeyNotFoundException();
        }
        product.name = name;
        product.image = image;
        product.colour = colour;
        product.price = price;
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

    public async Task<Account> NewAccount(string username, string password, string email)
    {
        foreach (var accountInList in _context.Accounts)
        {
            if (username == accountInList.Username)
            {
                throw new ArgumentException("Username already exists.");
            }
        }
        var account = new Account
        {
            Id = random.Next(),
            Username = username,
            Password = password,
            Email = email
        };
        var cart = new Cart
        {
            AccountId = account.Id,
            Products = new List<Product>()
        };
        await _context.Carts.AddAsync(cart);
        await _context.SaveChangesAsync();
        account.CartId = cart.Id;
        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();
        return account;
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
            Cart cart = await _context.Carts.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
            if (cart != null && cart.Products == null)
            {
                cart.Products = new List<Product>();
            }
            return cart;
        }
        catch
        {
            return null;
        }
    }

    public async Task UpdateCart(int accountId, int productId)
    {
        var cart = await _context.Carts.Include(c => c.Products).FirstOrDefaultAsync(c => c.AccountId == accountId);
        if (cart == null)
        {
            cart = new Cart
            {
                AccountId = accountId,
                Products = new List<Product>()
            };
            await _context.Carts.AddAsync(cart);
        }
        else if (cart.Products == null)
        {
            cart.Products = new List<Product>();
        }

        var product = cart.Products.FirstOrDefault(p => p.id == productId);
        if (product != null)
        {
            throw new Exception("Product already in cart");
        }
        else
        {
            product = await _context.Products.FirstOrDefaultAsync(p => p.id == productId);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }
            _logger.LogInformation(new EventId(0, "ProductAddedToCart"), $"Adding product {product.name} to cart {cart.Id}");

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

    public async Task NewDiscount(string code, float percentage)
    {
        var discount = new Discount
        {
            code = code,
            discount = percentage
        };
        await _context.Discounts.AddAsync(discount);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Discount>> GetDiscounts()
    {
        return await _context.Discounts.ToListAsync();
    }

    public async Task DeleteDiscount(int id)
    {
        Discount discount = await GetDiscount(id);
        _context.Discounts.Remove(discount);
        await _context.SaveChangesAsync();
    }

    public async Task<Discount> GetDiscount(int id)
    {
        try
        {
            Discount discount = await _context.Discounts.FindAsync(id);
            return discount;
        }
        catch
        {
            return null;
        }
    }

    public async Task UpdateDiscount(int id, string code, float percentage)
    {
        var discount = await _context.Discounts.FirstOrDefaultAsync(d => d.id == id);
        if (discount == null)
        {
            throw new KeyNotFoundException();
        }
        discount.code = code;
        discount.discount = percentage;
        _context.Discounts.Update(discount);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new KeyNotFoundException("Discount not found.");
        }
    }
}