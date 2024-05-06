namespace homework2.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        public static async ValueTask<Person?> BindAsync(HttpContext context)
        {
            // initialize new person
            var person = new Person();
            person.Id = -1; // so that it is not found by the handler
                            // (if all the parsing mechanisms below fail)

            // get all query string parameters
            var query = context.Request.Query;

            // try get value from route
            if (context.Request.RouteValues.ContainsKey("id"))
            {
                var id = context.Request.RouteValues["id"];
                if (id != null && int.TryParse(id.ToString(), out var parsedId)) person.Id = parsedId;
            }
            // try get value from query
            else if (query.TryGetValue("id", out var outId)
                && int.TryParse(outId, out var parsedId)) person.Id = parsedId;

            // try get value from route
            if (context.Request.RouteValues.ContainsKey("name")) person.Name = context.Request.RouteValues["name"].ToString();
            // try get value from query
            else if (query.TryGetValue("name", out var outName)) person.Name = outName;

            return person;
        }
    }
}