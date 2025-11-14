using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace quiz.hub.API.ActionFilters
{
    public class ExecutionTimeFilter(ILogger<ExecutionTimeFilter> _logger) : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var stopWatch = Stopwatch.StartNew();
            var resultContext = await next(); // Execute the action method
            stopWatch.Stop();

            var executionTime = stopWatch.ElapsedMilliseconds;
            _logger.LogInformation(100, "Action {ActionName} executed in: {ExecutionTime} ms",
                context.ActionDescriptor.DisplayName,
                executionTime);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Action {context.ActionDescriptor.DisplayName} executed in: {executionTime} ms",
                context.ActionDescriptor.DisplayName,
                executionTime);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
