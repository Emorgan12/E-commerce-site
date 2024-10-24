using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductsRepository
{
    private readonly DataContext _context;

    public ProductRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetProducts()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task NewProduct(string name, string colour, float price)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = name,
            Colour = colour,
            Price = price
        };
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProduct(Guid id)
    {
        Product product = await GetProduct(id);
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<Product> GetProduct(Guid id)
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
}