using homework2.Models;

namespace homework2.Filters
{
    public class PersonsValidationFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(
            EndpointFilterInvocationContext context,
            EndpointFilterDelegate next)
        {
            // get first argument
            var persons = context.GetArgument<List<Person>>(0);

            // loop the list
            foreach (var person in persons)
            {
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
                        {"Name", new[]{$"Name must start by a letter ! (for person of id=`{person.Id}`)"} }
                    });
                }
            }

            // continue to the next middleware in the pipeline
            return await next(context);
        }
    }
}
