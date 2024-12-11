using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using ECommerceSite;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http.HttpResults;


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

    public async Task NewProduct(string Pname, string Pimage, string Pcolour, float Pprice, string Pdescription, string Psize, string Pmaterial, string POriginCountry, int Pquantity, string Pcategory)
    {
        var product = new Product
        {
            name = Pname,
            image = Pimage,
            colour = Pcolour,
            price = Pprice,
            description = Pdescription,
            size = Psize,
            material = Pmaterial,
            originCountry = POriginCountry,
            quantity = Pquantity,
            category = Pcategory
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
            Product product = await _context.Products.Include(p => p.reviews).FirstOrDefaultAsync(p => p.id == id);
            return product;
        }
        catch
        {
            return null;
        }
    }

    public async Task UpdateProduct(int id, string Pname, string Pimage, string Pcolour, float Pprice, string Pdescription, string Psize, string Pmaterial, string POriginCountry, int Pquantity, string Pcategory)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.id == id);
        if (product == null)
        {
            throw new KeyNotFoundException();
        }
        product.name = Pname;
        product.image = Pimage;
        product.colour = Pcolour;
        product.price = Pprice;
        product.description = Pdescription;
        product.size = Psize;
        product.material = Pmaterial;
        product.originCountry = POriginCountry;
        product.quantity = Pquantity;
        product.category = Pcategory;
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

    public async Task<Account> NewAccount(string username, string password, string email, string Admin)
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
            Email = email,
            Admin = Admin
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

    public async Task<Review> NewReview(int rating, string review, int productId)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.id == productId);
        if (product == null)
        {
            throw new KeyNotFoundException("Product not found.");
        }
        var reviewObj = new Review
        {
            Rating = rating,
            review = review,
            reviewProductId = productId
        };
        await _context.Reviews.AddAsync(reviewObj);
        await _context.SaveChangesAsync();
        return reviewObj;
    }

    public async Task<Review> GetReview(int id)
    {
        try
        {
            var review = await _context.Reviews.FindAsync(id);
            return review;
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<Review>> GetReviews()
    {
        return await _context.Reviews.ToListAsync();
    }

    public async Task UpdateReview(int id, int rating, string review)
    {
        var Review = await _context.Reviews.FirstOrDefaultAsync(r => r.id == id);
        Review.review = review;
        Review.Rating = rating;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteReview(int id)
    {
        var review = await _context.Reviews.FirstOrDefaultAsync(r => r.id == id);
        if (review != null)
        {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }

    }

    public List<Product> getProductsFromList(List<int> ProductIds)
    {
        List<Product> productsList = new List<Product>();

        foreach (int id in ProductIds)
        {
            var product = _context.Products.FirstOrDefault(p => p.id == id);
            if (product != null)
            {
                productsList.Add(product);
                _logger.LogInformation($"Purchased product {product.name}");
            }
            else
            {
                return null;
            }
        }
        return productsList;
    }

    public async Task<String> NewOrder(List<int> ProductIds, int accountId)
    {
        var account = _context.Accounts.FirstOrDefault(a => a.Id == accountId);
        if (account != null)
        {
            var productsList = getProductsFromList(ProductIds);
            _logger.LogInformation("Products:");
            foreach (var product in productsList)
            {
                _logger.LogInformation(product.name);
            }
            var order = new Order
            {
                products = productsList,
                orderAccountId = accountId,
                dateCreated = DateTime.Now
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return "Order created successfully";
        }
        return "Account not found";
    }

    public async Task<List<Order>> GetOrders()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task<Order> GetOrder(int id)
    {
        try
        {
            var order = await _context.Orders.FindAsync(id);
            return order;
        }
        catch
        {
            return null;
        }
    }

    public async Task DeleteOrder(int id)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.id == id);
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }
}