using static System.Net.Mime.MediaTypeNames;

namespace MvcTodo.Middleware;

public class PerformanceMiddleware
{
    private readonly ILogger _logger;
    private readonly RequestDelegate _next;

    public PerformanceMiddleware(ILogger<PerformanceMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task Invoke(HttpContext context) {
        DateTime startTime = DateTime.UtcNow;

        await _next.Invoke(context);

        _logger.LogInformation($"Request to {context.Request.Path} took {Math.Round(DateTime.UtcNow.Subtract(startTime).TotalMilliseconds)}ms");
    }
}

public static class PerformanceExtensions
{
    public static IApplicationBuilder UsePerformance(this IApplicationBuilder app)
    {
        return app.UseMiddleware<PerformanceMiddleware>();
    }
}