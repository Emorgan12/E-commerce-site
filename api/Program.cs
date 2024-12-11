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
app.MapPut("/updateProduct", async (int id, string name, string image, string colour, float price, IProductsRepository repository) =>
{
    var product = repository.GetProduct(id);
    await repository.UpdateProduct(id, name, image, colour, price);
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
app.MapPost("/newAccount", async ([FromBody] Account account, IProductsRepository repository) =>
{
    await repository.NewAccount(account.Username, account.Password, account.Email);
    return Results.Ok(account);
})
.WithName("NewAccount")
.WithOpenApi();
app.MapPost("/postProduct", async ([FromBody] Product product, IProductsRepository repository) =>
{
    await repository.NewProduct(product.name, product.image, product.colour, product.price, product.description);
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

        public string category { get; set; }
    }

    public class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int CartId { get; set; }
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
}