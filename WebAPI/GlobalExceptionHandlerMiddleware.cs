using Microsoft.AspNetCore.Mvc;

namespace WebAPI;

public class GlobalExceptionHandlerMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(
        ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            var traceId = Guid.NewGuid();
            _logger.LogError(
                $"Error occure while processing the request, TraceId : ${traceId},\" +\n " +
                $" $\" Message : ${e.Message}, StackTrace: ${e.StackTrace}, Inner: ${e.InnerException}");
            
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var ProblemDetails = new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Title = "Internal Server Error",
                Status = (int)StatusCodes.Status500InternalServerError,
                Instance = context.Request.Path,
                Detail = $"Internal server error occured, traceId : {traceId}"
            };
            await context.Response.WriteAsJsonAsync(ProblemDetails);
        }
    }
}