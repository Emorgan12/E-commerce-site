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

app.MapPost("/postProduct", async ([FromBody] Product product, IProductsRepository repository) =>
{
    await repository.NewProduct(product.Name, product.Image, product.Colour, product.Price);
    return Results.Ok(product);
})
.WithName("NewProduct")
.WithOpenApi();

app.MapGet("/getProducts", async (IProductsRepository repository) =>
{
    var products = await repository.GetProducts();
    return Results.Ok(products);
})
.WithName("GetProducts")
.WithOpenApi();

app.MapDelete("/deleteProduct", async (Guid id, IProductsRepository repository, [FromServices] ILogger<Program> logger) =>
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

app.MapGet("/searchProduct", async (Guid id, IProductsRepository repository) =>
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

app.MapPut("/updateProduct", async (Guid id, string name, Uri image, string colour, float price, IProductsRepository repository) =>
{
    var product = repository.GetProduct(id);
    await repository.UpdateProduct(id, name, image, colour, price);
})
.WithName("UpdateProduct")
.WithOpenApi();

app.MapGet("/getAccounts", async (IProductsRepository repository) =>
{
    var accounts = await repository.GetAccounts();
    return Results.Ok(accounts);
})
.WithName("GetAccounts")
.WithOpenApi();

app.MapPost("/newAccount", async ([FromBody] Account account, IProductsRepository repository) =>
{
    await repository.NewAccount(account.Username, account.Password, account.Email);
    return Results.Ok(account);
})
.WithName("NewAccount")
.WithOpenApi();


app.MapPut("/updateEmail", async (Guid id, [FromBody] string email, IProductsRepository repository) =>
{
    await repository.UpdateEmail(id, email);
    return Results.NoContent();
})
.WithName("UpdateEmail")
.WithOpenApi();

app.MapPut("/updatePassword", async (Guid id, [FromBody] string password, IProductsRepository repository) =>
{
    await repository.UpdatePassword(id, password);
    return Results.NoContent();
})
.WithName("UpdatePassword")
.WithOpenApi();

app.MapDelete("/deleteAccount", async (Guid id, IProductsRepository repository) =>
{
    await repository.DeleteAccount(id);
    return Results.NoContent();
})
.WithName("DeleteAccount")
.WithOpenApi();

app.MapGet("/getAccount", async (Guid id, IProductsRepository repository) =>
{
    var account = await repository.GetAccount(id);
    return account;
})
.WithName("GetAccount")
.WithOpenApi();

app.Run();

public class Product
{
    public Guid Id { get; set; }

    public Uri Image { get; set; }
    public string Name { get; set; }
    public string Colour { get; set; }
    public float Price { get; set; }
}

public class Account
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}