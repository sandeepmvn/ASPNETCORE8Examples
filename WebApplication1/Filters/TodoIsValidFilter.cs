
using Microsoft.AspNetCore.Components.Forms;
using WebApplication1.Model;

namespace WebApplication1.Filters
{
    public class TodoIsValidFilter : IEndpointFilter
    {
        private ILogger _logger;
        public TodoIsValidFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TodoIsValidFilter>();
        }
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            //before
            var todo = context.GetArgument<Todo>(0);
            if (string.IsNullOrEmpty(todo.Name) || string.IsNullOrWhiteSpace(todo.Name))
            {
                return Results.Problem("Name is required");
            }
            if (todo.Name.Length > 5)
            {
                return Results.Problem("Name length > 5");
            }
            var result= await next(context);
            //After
            return result;
        }
    }
}
