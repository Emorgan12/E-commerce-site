

public interface IProductsRepository
{
    Task<List<Product>> GetProducts();
    Task NewProduct(string name, string colour, float price);

    Task DeleteProduct(Guid id);

    Task<Product> GetProduct(Guid id);

}