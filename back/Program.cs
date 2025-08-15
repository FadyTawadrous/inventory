// Program.cs
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add in-memory database for demonstration purposes
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseInMemoryDatabase("InventoryDb"));

builder.Services.AddOpenApi();
builder.Services.AddControllers();

// Add CORS (Cross-Origin Resource Sharing Service) policy
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();
app.UseRouting();
app.MapControllers();

// Ensure the database is created on application start
// app.Lifetime.ApplicationStarted.Register(() =>
// {
//     using var scope = app.Services.CreateScope();
//     var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//     dbContext.Database.EnsureCreated();
// });

// Map endpoints
app.MapGet("/", () => "Hello World!");

// app.MapGet("/products",
//     () => new[] { new { Id = 1, Name = "Product 1", Price = 70.0m }, new { Id = 2, Name = "Product 2", Price = 50.0m } });

List<Product> products = new List<Product>();
products.Add(new Product { Id = 1, Name = "Product 1", Price = 70.0m });

// Map to add a new product
app.MapPost("/products", (Product product) =>
{
    Product newProduct = new Product
    {
        Id = products.Count + 1,
        Name = product.Name,
        Price = product.Price
    };
    products.Add(newProduct);
    return Results.Created($"/products/{newProduct.Id}", newProduct);
});

// Map to get all products
app.MapGet("/products", () => Results.Ok(products));

// Map to get a product by ID
app.MapGet("/products/{id:int}", (int id) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);
    if (product == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(product);
});

app.Run();
