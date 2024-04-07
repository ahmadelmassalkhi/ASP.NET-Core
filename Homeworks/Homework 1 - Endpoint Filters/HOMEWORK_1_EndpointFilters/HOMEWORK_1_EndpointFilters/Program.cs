using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// routes to endpoints
app.MapGet("/Students", Handlers.GetAllStudents);
app.MapGet("/Students/{id}", Handlers.GetStudent);

app.MapPost("/Students/{id}", Handlers.AddStudent)
    .AddEndpointFilter(ValidationHelper.ValidId) // static method
    .AddEndpointFilter<GradeValidation>(); // implements IEndpointFilter interface

app.MapPut("/Students/{id}", Handlers.ModifyStudent);
app.MapGet("/Students/GetAverage", Handlers.GetAverageOfAllStudents);
app.MapGet("/Students/GetInfo", Handlers.GetInfo);

app.Run();


class Student
{
    public string? Name { get; set; }
    public int Age { get; set; }
    public string? Nationality { get; set; }
    public int Grade { get; set; }
}

record StudentList()
{
    public static readonly ConcurrentDictionary<int, Student> AllStudents = new();
}

static class Handlers
{
    public static IResult GetAllStudents()
    {
        return Results.Ok(StudentList.AllStudents);
    }

    public static IResult GetStudent(int id)
    {
        if (StudentList.AllStudents.ContainsKey(id) == false)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]> {
                {"id", new[] {"There is no student with this id !"} }
            });
        }
        return Results.Ok(StudentList.AllStudents[id]);
    }

    public static IResult AddStudent(int id, [FromBody] Student student)
    {
        // validate
        if (StudentList.AllStudents.ContainsKey(id))
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                { "id", new[] {"id already exists !"} }
            });
        }

        // add
        if (!StudentList.AllStudents.TryAdd(id, student))
        {
            return Results.Problem(
                detail: "Failed to add new student, please try again !",
                statusCode: 500,
                title: "Internal Server Error");
        }
        return TypedResults.Created($"/Students/{id}", student);
    }

    public static IResult ModifyStudent(int id, [FromBody] Student student)
    {
        if (StudentList.AllStudents.ContainsKey(id) == false)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                { "id", new[] {"There is no student with the given id !"} }
            });
        }
        StudentList.AllStudents[id] = student;
        return Results.Ok();
    }

    public static IResult GetAverageOfAllStudents()
    {
        double sum = 0;
        foreach (var s in StudentList.AllStudents)
        {
            sum += s.Value.Grade;
        }
        return (sum == 0) ?
            Results.Ok(0) :
            Results.Ok(sum / StudentList.AllStudents.Count);
    }

    public static IResult GetInfo()
    {
        List<object> list = [];
        foreach (var key in StudentList.AllStudents.Keys)
        {
            Student s = StudentList.AllStudents[key];
            var obj = new
            {
                id = key,
                name = s.Name,
                nationality = s.Nationality
            };
            list.Add(obj);
        }

        return Results.Ok(list);
    }
}


internal class ValidationHelper
{
    public static async ValueTask<object?> ValidId(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        // get argument
        var id = context.GetArgument<int>(0);

        // validate
        if (id < 0 || id >= 1000)
        {
            // return BadRequest + problem details
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                {"id", new[]{ "id must be positive and less than 1000" } }
            });
        }

        // proceed to next Filter|Endpoint (if no filters left)
        return await next(context);
    }
}


class GradeValidation : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        // get argument
        var grade = context.GetArgument<Student>(1).Grade;

        // validate
        if (grade < 0 || grade > 100)
        {
            // return BadRequest + problem details
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                { "Grade", new[]{"Grade must be between 0 and 100"} }
            });
        }

        // proceed to Next-Filter | Endpoint (if no filters left)
        return await next(context);
    }
}