using homework2.Models;

namespace homework2.Filters
{
    public class PersonValidationFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
        {
            // get first argument
            var person = context.GetArgument<Person>(0);

            // validate id
            if (person.Id < 1 || person.Id > 1000)
            {
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    {"Id", new[]{$"Id must be between 1 and 1000, recieved: `{person.Id}`"} }
                });
            }

            // validate name
            if (string.IsNullOrEmpty(person.Name) || !char.IsLetter(person.Name[0]))
            {
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    {"Name", new[]{$"Name must start by a letter !"} }
                });
            }

            // continue to the next middleware in the pipeline
            return await next(context);
        }
    }
}
