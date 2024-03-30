using homework2.Models;

namespace homework2.Filters
{
    public class IdValidationFilter(List<Person> people) : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(
            EndpointFilterInvocationContext context, 
            EndpointFilterDelegate next)
        {
            // get first argument
            var id = context.GetArgument<int?>(0);
            
            // validate
            if (id is null || id < 1 || id > 1000)
            {
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    {"id", new[]{"invalid format, id must be between 1 and 1000"} }
                });
            }

            // check invalid id
            var person = people.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    {"Id", new[]{$"Does not exist a person with id=`{id}` !"} }
                });
            }

            // continue to next middleware in the pipeline
            return await next(context);
        }
    }
}