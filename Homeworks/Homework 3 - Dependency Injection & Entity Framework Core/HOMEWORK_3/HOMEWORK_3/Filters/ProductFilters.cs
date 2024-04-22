using HOMEWORK_3.Data;
using HOMEWORK_3.Requests;
using Microsoft.EntityFrameworkCore;

namespace HOMEWORK_3.Filters
{
    public class ProductFilters(DataContext context) : IEndpointFilter
    {
        public DataContext _context = context;

        public async ValueTask<object?> InvokeAsync(
            EndpointFilterInvocationContext context,
            EndpointFilterDelegate next)
        {
            // get data from argument
            var requestProduct = context.GetArgument<ProductRequest>(0);

            // validate CategoryId
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == requestProduct.CategoryId);
            if (category == null)
            {
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    {"CategoryId", new[] {$"There is no category with such CategoryId `{requestProduct.CategoryId}` !"} }
                });
            }

            // validate Price
            if (requestProduct.Price <= 0)
            {
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    {"Price", new[] {$"Price should be greater than 0 !"} }
                });
            }

            // continue to next middleware in the pipeline
            return await next(context);
        }
    }
}
