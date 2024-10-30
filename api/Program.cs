using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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

app.MapPost("/post", async ([FromBody] Product product, IProductsRepository repository) =>
{
    await repository.NewProduct(product.Name, product.Image, product.Colour, product.Price);
    return Results.Ok(product);
})
.WithName("NewProduct")
.WithOpenApi();

app.MapGet("/get", async (IProductsRepository repository) =>
{
    var products = await repository.GetProducts();
    return Results.Ok(products);
})
.WithName("GetProducts")
.WithOpenApi();

app.MapDelete("/delete", async (Guid id, IProductsRepository repository, [FromServices] ILogger<Program> logger) =>
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

app.MapGet("/search", async (Guid id, IProductsRepository repository) =>
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

app.MapPut("/update", async (Guid id, string name, Uri image, string colour, float price, IProductsRepository repository) =>
{
    var product = repository.GetProduct(id);
    await repository.UpdateProduct(id, name, image, colour, price);
})
.WithName("UpdateProduct")
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