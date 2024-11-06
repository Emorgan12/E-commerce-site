

using System.ComponentModel.DataAnnotations;

public interface IProductsRepository
{
    Task<List<Product>> GetProducts();
    Task NewProduct(string name, Uri image, string colour, float price);

    Task DeleteProduct(Guid id);

    Task<Product> GetProduct(Guid id);

    Task UpdateProduct(Guid id, string name, Uri image, string colour, float price);

    Task NewAccount(string username, string password, string email);
    Task<List<Account>> GetAccounts();
    Task DeleteAccount(Guid id);

    Task<Account> GetAccount(Guid id);
    Task UpdatePassword(Guid id, string password);
    Task UpdateEmail(Guid id, string email);

}