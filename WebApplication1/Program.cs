using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Filters;
using WebApplication1.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/Filters", () =>
{
    app.Logger.LogInformation("             Endpoint");
    return "Test of multiple filters";
})
    .AddEndpointFilter(async (efiContext, next) =>
    {
        app.Logger.LogInformation("Before first filter");
        var result = await next(efiContext);
        app.Logger.LogInformation("After first filter");
        return result;
    })
    .AddEndpointFilter(async (efiContext, next) =>
    {
        app.Logger.LogInformation(" Before 2nd filter");
        var result = await next(efiContext);
        app.Logger.LogInformation(" After 2nd filter");
        return result;
    })
    .AddEndpointFilter(async (efiContext, next) =>
    {
        app.Logger.LogInformation("     Before 3rd filter");
        var result = await next(efiContext);
        app.Logger.LogInformation("     After 3rd filter");
        return result;
    }).AddEndpointFilter<LogEndpointFilters>().WithTags("Filters");






app.MapGet("/", () => "Hello World!");
app.MapGet("/SayHello/{name}", (string name) => $"Hello {name}");
app.MapGet("/users/{userId}/books/{bookId}",
    (int userId, int bookId) => $"The user id is {userId} and book id is {bookId}");
var api = app.MapGroup("/api").WithOpenApi();




var todoitem = api.MapGroup("/todoitem").WithTags(nameof(Todo));
todoitem.MapGet("/todoitems", async (TodoDb db) =>
    await db.Todos.ToListAsync());

todoitem.MapGet("/todoitems/completed", GetCompletedItems);
async Task<List<Todo>> GetCompletedItems(HttpContext context, [FromServices] TodoDb db)
{
    return await db.Todos.Where(x => x.IsComplete).ToListAsync();
}


todoitem.MapGet("/todoitems/{id}", async (int id, TodoDb db) =>
           await db.Todos.FindAsync(id) is Todo todo ? Results.Ok(todo) : Results.NotFound());

todoitem.MapPost("/todoitems", async (Todo todo, TodoDb db) =>
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();

    return Results.Created($"/todoitems/{todo.Id}", todo);
}).AddEndpointFilter<TodoIsValidFilter>();
//    .AddEndpointFilter(async (context, next) =>
//{
//    //before
//    var todo = context.GetArgument<Todo>(0);
//    if (string.IsNullOrEmpty(todo.Name) || string.IsNullOrWhiteSpace(todo.Name))
//    {
//        return Results.Problem("Name is required");
//    }
//    if (todo.Name.Length > 5)
//    {
//        return Results.Problem("Name length > 5");
//    }
//    var result= await next(context);
//    //After
//    return result;
//}); 

todoitem.MapPut("/todoitems/{id}", async (int id, Todo inputTodo, TodoDb db) =>
{
    var todo = await db.Todos.FindAsync(id);

    if (todo is null) return Results.NotFound();

    todo.Name = inputTodo.Name;
    todo.IsComplete = inputTodo.IsComplete;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

todoitem.MapDelete("/todoitems/{id}", async (int id, TodoDb db) =>
{
    if (await db.Todos.FindAsync(id) is Todo todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

api.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi().WithTags("GetWeatherForecast");

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
