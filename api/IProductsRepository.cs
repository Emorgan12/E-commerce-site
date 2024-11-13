

using System.ComponentModel.DataAnnotations;

public interface IProductsRepository
{
    Task<List<Product>> GetProducts();
    Task NewProduct(string name, Uri image, string colour, float price);
    Task DeleteProduct(int id);
    Task<Product> GetProduct(int id);
    Task UpdateProduct(int id, string name, Uri image, string colour, float price);

    Task NewAccount(string username, string password, string email);
    Task<List<Account>> GetAccounts();
    Task DeleteAccount(int id);
    Task<Account> GetAccount(int id);
    Task UpdatePassword(int id, string password);
    Task UpdateEmail(int id, string email);
    
    Task NewCart(int accountId);
    Task<List<Cart>> GetCarts();
    Task DeleteCart(int id);
    Task<Cart> GetCart(int id);
    Task UpdateCart(int id, int accountId, Product product);
}