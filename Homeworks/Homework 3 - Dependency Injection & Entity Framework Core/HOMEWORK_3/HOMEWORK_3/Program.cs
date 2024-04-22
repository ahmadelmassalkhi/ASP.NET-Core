using HOMEWORK_3.Data;
using HOMEWORK_3.Filters;
using HOMEWORK_3.Requests;
using HOMEWORK_3.Services.Implementations;
using HOMEWORK_3.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*********************************************************************/
// Adding services & database ConnectionString
builder.Services.AddScoped<IProductService, ProductService>()
                .AddScoped<ICategoryService, CategoryService>()
                .AddDbContext<DataContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
/*********************************************************************/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

/*********************************************************************/

// Products
var products = app.MapGroup("/products").WithTags("Products");

// CREATE
products.MapPost("/", async (
    ProductRequest product,
    IProductService service) =>
{
    return await service.CreateProduct(product);
}).AddEndpointFilter<ProductFilters>()
  .WithParameterValidation();

// READ
products.MapGet("/", async (
    decimal? minPrice,
    decimal? maxPrice,
    string? name,
    string? description,
    string? categoryName,
    IProductService service) =>
{
    return await service.GetAllProducts(
        minPrice,
        maxPrice,
        name,
        description,
        categoryName);
});

// UPDATE
products.MapPut("/{id}", async (
    ProductRequest product,
    int id,
    IProductService service) =>
{
    return await service.UpdateProduct(id, product);
}).WithParameterValidation();

// DELETE
products.MapDelete("/{id}", async (int id, IProductService service) => await service.DeleteProduct(id));

// additional
products.MapGet("/{id}", async (int id, IProductService service) => await service.GetProductById(id));
products.MapGet("/average", async (IProductService service) => await service.GetAverage());

/*********************************************************************/

// Categories
var categories = app.MapGroup("/categories").WithTags("Categories");

// CREATE
categories.MapPost("/", async (
    CategoryRequest category,
    ICategoryService service) =>
{
    return await service.CreateCategory(category);
}).WithParameterValidation();

// READ
categories.MapGet("/", async (ICategoryService service) => await service.GetAllCategories());

// UPDATE
categories.MapPut("/{id}", async (
    int id,
    CategoryRequest category,
    ICategoryService service) =>
{
    return await service.UpdateCategory(id, category);
}).WithParameterValidation();

// DELETE
categories.MapDelete("/{id}", async (int id, ICategoryService service) => await service.DeleteCategory(id));

// additional
categories.MapGet("/{id}", async (int id, ICategoryService service) => await service.GetCategoryById(id));
categories.MapGet("/withNumberOfProducts", async (ICategoryService service) => await service.GetCategoriesWithNumberOfProducts());

/*********************************************************************/

app.Run();
