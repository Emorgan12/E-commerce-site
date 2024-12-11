using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using ECommerceSite;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();


// Register DataContext with dependency injection to use SQLite
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register the repository
builder.Services.AddScoped<IProductsRepository, ProductRepository>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyHeader();
    options.AllowAnyMethod();
});

app.MapDelete("/deleteDiscount", async (int id, IProductsRepository repository) =>
{
    await repository.DeleteDiscount(id);
    return Results.NoContent();
})
.WithName("DeleteDiscount")
.WithOpenApi();
app.MapDelete("/deleteProduct", async (int id, IProductsRepository repository, [FromServices] ILogger<Program> logger) =>
{
    try
    {
        await repository.DeleteProduct(id);
        return Results.NoContent();
    }
    catch
    {
        return Results.NotFound();
    }
})
.WithName("DeleteProduct")
.WithOpenApi();
app.MapDelete("/deleteAccount", async (int id, IProductsRepository repository) =>
{
    await repository.DeleteAccount(id);
    return Results.NoContent();
})
.WithName("DeleteAccount")
.WithOpenApi();

app.MapDelete("/deleteCart", async (int id, IProductsRepository repository) =>
{
    await repository.DeleteCart(id);
    return Results.NoContent();
})
.WithName("DeleteCart")
.WithOpenApi();

app.MapDelete("/deleteReview", async (int id, IProductsRepository repository) =>
{
    await repository.DeleteReview(id);
    return Results.NoContent();
})
.WithName("DeleteReview")
.WithOpenApi();

app.MapDelete("/deleteOrder", async (int id, IProductsRepository repository) =>
{
    await repository.DeleteOrder(id);
    return Results.NoContent;
})
.WithName("DeleteOrder")
.WithOpenApi();

app.MapPut("/updateProduct", async (int id, string name, string image, string colour, float price, string description, string size, string material, string OriginCountry, int quantity, string category, IProductsRepository repository) =>
{
    var product = repository.GetProduct(id);
    await repository.UpdateProduct(id, name, image, colour, price, description, size, material, OriginCountry, quantity, category);
})
.WithName("UpdateProduct")
.WithOpenApi();
app.MapPut("/updateEmail", async (int id, [FromBody] string email, IProductsRepository repository) =>
{
    await repository.UpdateEmail(id, email);
    return Results.NoContent();
})
.WithName("UpdateEmail")
.WithOpenApi();

app.MapPut("/updatePassword", async (int id, [FromBody] string password, IProductsRepository repository) =>
{
    await repository.UpdatePassword(id, password);
    return Results.NoContent();
})
.WithName("UpdatePassword")
.WithOpenApi();
app.MapPut("/updateCart", async (int accountId, int productId, IProductsRepository repository) =>
{
    await repository.UpdateCart(accountId, productId);
    return Results.NoContent();
})
.WithName("UpdateCart")
.WithOpenApi();
app.MapPut("/updateDiscount", async (int id, [FromBody] Discount discount, IProductsRepository repository) =>
{
    await repository.UpdateDiscount(id, discount.code, discount.discount);
    return Results.NoContent();
})
.WithName("UpdateDiscount")
.WithOpenApi();

app.MapPut("/updateReview", async (int id, int rating, string review, IProductsRepository repository) =>
{
    await repository.UpdateReview(id, rating, review);

})
.WithName("UpdateReview")
.WithOpenApi();

app.MapPost("/newAccount", async ([FromBody] Account account, IProductsRepository repository) =>
{
    await repository.NewAccount(account.Username, account.Password, account.Email, account.Admin);
    return Results.Ok(account);
})
.WithName("NewAccount")
.WithOpenApi();
app.MapPost("/postProduct", async ([FromBody] Product product, IProductsRepository repository) =>
{
    await repository.NewProduct(product.name, product.image, product.colour, product.price, product.description, product.size, product.material, product.originCountry, product.quantity, product.category);
    return Results.Ok(product);
})
.WithName("NewProduct")
.WithOpenApi();
app.MapPost("/postDiscount", async ([FromBody] Discount discount, IProductsRepository repository) =>
{
    await repository.NewDiscount(discount.code, discount.discount);
    return Results.Ok(discount);
})
.WithName("NewDiscount")
.WithOpenApi();

app.MapPost("/postReview", async ([FromBody] Review review, IProductsRepository repository) =>
{
    await repository.NewReview(review.Rating, review.review, review.reviewProductId);
    return Results.Ok(review);
})
.WithName("NewReview")
.WithOpenApi();

app.MapPost("/postOrder", async (List<int> productIds, int accountId, IProductsRepository repository) =>
{
    var order = await repository.NewOrder(productIds, accountId);
    return order;

})
.WithName("NewOrder")
.WithOpenApi();

app.MapGet("/getProducts", async (IProductsRepository repository) =>
{
    var products = await repository.GetProducts();
    return Results.Ok(products);
})
.WithName("GetProducts")
.WithOpenApi();


app.MapGet("/searchProduct", async (int id, IProductsRepository repository) =>
{
    var product = await repository.GetProduct(id);
    if (product != null)
    {
        return Results.Ok(product);
    }
    return Results.NotFound();
})
.WithName("GetProduct")
.WithOpenApi();


app.MapGet("/getAccounts", async (IProductsRepository repository) =>
{
    var accounts = await repository.GetAccounts();
    return Results.Ok(accounts);
})
.WithName("GetAccounts")
.WithOpenApi();

app.MapGet("/login/{username},{password}", async (string username, string password, IProductsRepository repository) =>
{
    var accounts = await repository.GetAccounts();
    foreach (var account in accounts)
    {
        if (account.Username == username && account.Password == password)
        {
            return Results.Ok(account);
        }
    }
    return Results.NotFound();
})
.WithName("Login")
.WithOpenApi();

app.MapGet("/getAccount", async (int id, IProductsRepository repository) =>
{
    var account = await repository.GetAccount(id);
    return account;
})
.WithName("GetAccount")
.WithOpenApi();


app.MapGet("/getCarts", async (IProductsRepository repository) =>
{
    var carts = await repository.GetCarts();
    return Results.Ok(carts);
})
.WithName("GetCarts")
.WithOpenApi();

app.MapGet("/getCart", async (int id, IProductsRepository repository) =>
{
    var cart = await repository.GetCart(id);
    return Results.Ok(cart);
})
.WithName("GetCart")
.WithOpenApi();

app.MapGet("/getDiscounts", async (IProductsRepository repository) =>
{
    var discounts = await repository.GetDiscounts();
    return Results.Ok(discounts);
})
.WithName("GetDiscounts")
.WithOpenApi();

app.MapGet("/getDiscount", async (int id, IProductsRepository repository) =>
{
    var discount = await repository.GetDiscount(id);
    if (discount != null)
    {
        return Results.Ok(discount);
    }
    return Results.NotFound();
})
.WithName("GetDiscount")
.WithOpenApi();

app.MapGet("/getReviews", async (IProductsRepository repository) =>
{

    var reviews = await repository.GetReviews();
    return Results.Ok(reviews);
})
.WithName("GetReviews")
.WithOpenApi();

app.MapGet("/getReview", async (int id, IProductsRepository repository) =>
{
    var review = await repository.GetReview(id);
    if (review != null)
    {
        return Results.Ok(review);
    }
    return Results.NotFound();
})
.WithName("GetReview")
.WithOpenApi();

app.MapGet("/getOrders", async (IProductsRepository repository) =>
{
    var orders = await repository.GetOrders();
    return Results.Ok(orders);
})
.WithName("GetOrders")
.WithOpenApi();

app.MapGet("/getOrder", async (int id, IProductsRepository repository) =>
{
    var order = await repository.GetOrder(id);
    if (order != null)
    {
        return Results.Ok(order);
    }
    return Results.NotFound();
})
.WithName("GetOrder")
.WithOpenApi();

app.Run();

namespace ECommerceSite
{
    public class Product
    {
        public int id { get; set; }

        public string image { get; set; }
        public string name { get; set; }
        public string colour { get; set; }
        public float price { get; set; }
        public string description { get; set; }
        public string size { get; set; }
        public string material { get; set; }
        public string originCountry { get; set; }
        public int quantity { get; set; }
        public string category { get; set; }
        [JsonIgnore]
        public List<Review> reviews { get; set; }

    }

    public class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int CartId { get; set; }
        public string Admin { get; set; }
        [JsonIgnore]
        public List<Order> orders { get; set; }
    }

    public class Cart
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public List<Product> Products { get; set; }
    }

    public class Discount
    {
        public int id { get; set; }
        public string code { get; set; }
        public float discount { get; set; }
    }

    public class Review
    {
        public int id { get; set; }
        public int Rating { get; set; }
        public string review { get; set; }
        public int reviewProductId { get; set; }
        public int orderId { get; set; }
    }

    public class Order
    {
        public int id { get; set; }
        public List<Product> products { get; set; }
        public int orderAccountId { get; set; }
        public DateTime dateCreated { get; set; }
    }
}