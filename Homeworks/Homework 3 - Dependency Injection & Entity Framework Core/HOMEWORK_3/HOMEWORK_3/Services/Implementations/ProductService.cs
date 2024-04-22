using HOMEWORK_3.Models;
using HOMEWORK_3.Data;
using HOMEWORK_3.Requests;
using HOMEWORK_3.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HOMEWORK_3.Services.Implementations
{
    public class ProductService(DataContext context) : IProductService
    {
        public readonly DataContext _context = context;

        public async Task<IResult> CreateProduct(ProductRequest request)
        {
            // copy information from request into product variable
            Product p = new()
            {
                Description = request.Description,
                Name = request.Name,
                Price = request.Price,
                CategoryId = request.CategoryId
            };

            // add & commit changes
            await _context.Products.AddAsync(p);
            await _context.SaveChangesAsync();
            return Results.Created($"/products/{p.ProductId}", p);
        }

        public async Task<IResult> GetAllProducts(
            decimal? minPrice,
            decimal? maxPrice,
            string? name,
            string? description,
            string? categoryName)
        {
            // get all products
            var products = _context.Products
                .Include(p => p.Category) // Eager loading the navigation property
                .AsQueryable();

            // Apply filters based on the parameters
            if (minPrice.HasValue) products = products.Where(p => p.Price >= minPrice);
            if (maxPrice.HasValue) products = products.Where(p => p.Price <= maxPrice);
            if (!string.IsNullOrEmpty(name)) products = products.Where(p => p.Name.Contains(name));
            if (!string.IsNullOrEmpty(description)) products = products.Where(p => p.Description != null && p.Description.Contains(description));
            if (!string.IsNullOrEmpty(categoryName)) products = products.Where(p => p.Category.Name.Contains(categoryName));

            // execute query and return result
            return Results.Ok(await products.ToListAsync());
        }

        public async Task<IResult> UpdateProduct(int id, ProductRequest request)
        {
            // get product
            var p = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            if (p == null) return Results.NotFound($"Id `{id}` doesn't correspond to any existing product !");

            // update product & commit changes
            p.Update(request.Name, request.Description, request.Price, request.CategoryId);
            await _context.SaveChangesAsync();

            return Results.Ok(p);
        }

        public async Task<IResult> GetProductById(int id)
        {
            var p = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);
            if (p == null) return Results.NotFound($"Id `{id}` doesn't correspond to any existing product !");
            return Results.Ok(p);
        }

        public async Task<IResult> DeleteProduct(int id)
        {
            // find product
            var p = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            if (p == null) return Results.NotFound($"Id `{id}` doesn't correspond to any existing product !");

            // delete product & committ changes
            _context.Products.Remove(p);
            await _context.SaveChangesAsync();

            return Results.Ok();
        }

        public async Task<IResult> GetAverage()
        {
            var products = await _context.Products.ToListAsync();
            if (products.Count == 0) return Results.Ok(0);
            return Results.Ok(products.Average(x => x.Price));
        }
    }
}
