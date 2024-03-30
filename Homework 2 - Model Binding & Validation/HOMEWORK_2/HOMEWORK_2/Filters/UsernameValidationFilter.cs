using homework2.Models;

namespace homework2.Filters
{
    public class UsernameValidationFilter(List<User> users) : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(
            EndpointFilterInvocationContext context,
            EndpointFilterDelegate next)
        {
            // get first argument
            var username = context.GetArgument<string>(0);

            // validate
            if (username == null || username.Length == 0 || username.Length > 20)
            {
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    {"Username", new[] {"Username must contains at least a letter, and at most 20 letters !"} }
                });
            }

            // check if user exists
            // we can place this check inside the handler for better time complexity
            var user = users.FirstOrDefault(u => u.Username.Equals(username));
            if (user == null)
            {
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    {"Username", new[] {$"Doesn't exist a User with such username `{username}` !"} }
                });
            }

            // continue to the next middleware in the pipeline
            return await next(context);
        }
    }
}
