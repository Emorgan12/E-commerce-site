

public interface IProductsRepository
{
    Task<List<Product>> GetProducts();
    Task NewProduct(string name, Uri image, string colour, float price);

    Task DeleteProduct(Guid id);

    Task<Product> GetProduct(Guid id);

    Task UpdateProduct(Guid id, string name, Uri image, string colour, float price);

}