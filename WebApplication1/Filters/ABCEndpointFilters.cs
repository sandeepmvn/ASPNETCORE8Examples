
using Microsoft.Extensions.Logging;

namespace WebApplication1.Filters
{
    public class LogEndpointFilters : IEndpointFilter
    {
        protected readonly ILogger Logger;
        private readonly string _methodName;
        public LogEndpointFilters(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger<LogEndpointFilters>();
            _methodName = GetType().Name;
        }
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            Logger.LogInformation("{MethodName} Before next", _methodName);
            var result = await next(context);
            Logger.LogInformation("{MethodName} After next", _methodName);
            return result;
        }
    }
}
