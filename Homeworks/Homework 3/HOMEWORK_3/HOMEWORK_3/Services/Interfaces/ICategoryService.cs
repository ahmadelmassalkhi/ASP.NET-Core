using HOMEWORK_3.Requests;

namespace HOMEWORK_3.Services.Interfaces
{
    public interface ICategoryService
    {
        // CRUD operations
        public Task<IResult> CreateCategory(CategoryRequest category);
        public Task<IResult> GetAllCategories();
        public Task<IResult> UpdateCategory(int id, CategoryRequest category);
        public Task<IResult> DeleteCategory(int id);

        // additional
        public Task<IResult> GetCategoryById(int id);
        public Task<IResult> GetCategoriesWithNumberOfProducts();
    }
}
