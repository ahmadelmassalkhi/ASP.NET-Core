using HOMEWORK_3.Data;
using HOMEWORK_3.Models;
using HOMEWORK_3.Requests;
using HOMEWORK_3.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HOMEWORK_3.Services.Implementations
{
    public class CategoryService(DataContext context) : ICategoryService
    {
        public readonly DataContext _context = context;

        public async Task<IResult> CreateCategory(CategoryRequest category)
        {
            Category c = new();
            c.Name = category.Name;
            await _context.Categories.AddAsync(c);
            await _context.SaveChangesAsync();
            return Results.Created($"/categories/{c.CategoryId}", c);
        }

        public async Task<IResult> GetAllCategories()
        {
            return Results.Ok(await _context.Categories.ToListAsync());
        }

        public async Task<IResult> UpdateCategory(int id, CategoryRequest category)
        {
            var c = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            if (c == null) return Results.NotFound($"Id `{id}` doesn't correspond to any existing category !");
            c.Name = category.Name;
            await _context.SaveChangesAsync();
            return Results.Ok();
        }

        public async Task<IResult> DeleteCategory(int id)
        {
            // get category
            var c = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            if (c == null) return Results.NotFound($"Id `{id}` doesn't correspond to any existing category !");

            // delete category
            _context.Categories.Remove(c);

            // commit & return
            await _context.SaveChangesAsync();
            return Results.Ok();
        }

        public async Task<IResult> GetCategoryById(int id)
        {
            var c = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            if (c == null) return Results.NotFound($"Id `{id}` doesn't correspond to any existing category !");
            return Results.Ok(c);
        }

        public async Task<IResult> GetCategoriesWithNumberOfProducts()
        {
            var result = await _context.Categories
                .Select(c => new
                {
                    c.CategoryId,
                    c.Name,
                    nbOfProducts = _context.Products.Count(p => p.CategoryId == c.CategoryId)
                }).ToListAsync();
            return Results.Ok(result);
        }
    }
}
