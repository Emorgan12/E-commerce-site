

using System.ComponentModel.DataAnnotations;
using ECommerceSite;

public interface IProductsRepository
{
    Task<List<Product>> GetProducts();
    Task NewProduct(string name, string image, string colour, float price, string description, string size, string material, string OriginCountry, int quantity, string category);
    Task DeleteProduct(int id);
    Task<Product> GetProduct(int id);
    Task UpdateProduct(int id, string name, string image, string colour, float price, string description, string size, string material, string OriginCountry, int quantity, string category);

    Task<Account> NewAccount(string username, string password, string email, bool Admin);
    Task<List<Account>> GetAccounts();
    Task DeleteAccount(int id);
    Task<Account> GetAccount(int id);
    Task UpdatePassword(int id, string password);
    Task UpdateEmail(int id, string email);

    Task<List<Cart>> GetCarts();
    Task DeleteCart(int id);
    Task<Cart> GetCart(int id);
    Task UpdateCart(int accountId, int productId);

    Task NewDiscount(string code, float percentage);
    Task<List<Discount>> GetDiscounts();
    Task DeleteDiscount(int id);
    Task<Discount> GetDiscount(int id);
    Task UpdateDiscount(int id, string code, float percentage);

    Task<Review> NewReview(int rating, string review, int productId);
    Task<List<Review>> GetReviews();
    Task<Review> GetReview(int id);
    Task DeleteReview(int id);
    Task UpdateReview(int id, int rating, string review);

    Task<String> NewOrder(List<int> productIds, int accountId);
    Task<List<Order>> GetOrders();
    Task<Order> GetOrder(int id);
    Task DeleteOrder(int id);

}