using HOMEWORK_3.Requests;

namespace HOMEWORK_3.Services.Interfaces
{
    public interface IProductService
    {
        // CRUD operations
        public Task<IResult> CreateProduct(ProductRequest request);
        public Task<IResult> GetAllProducts(
            decimal? minPrice,
            decimal? maxPrice,
            string? name,
            string? description,
            string? categoryName);
        public Task<IResult> UpdateProduct(int id, ProductRequest request);
        public Task<IResult> DeleteProduct(int id);

        // additional
        public Task<IResult> GetProductById(int id);
        public Task<IResult> GetAverage();
    }
}
