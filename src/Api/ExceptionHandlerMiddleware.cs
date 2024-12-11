using Microsoft.AspNetCore.Diagnostics;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api
{
    public class ExceptionHandlerMiddleware(
        ILogger<ExceptionHandlerMiddleware> logger) : IExceptionHandler
    { 
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var exceptionMessage = exception.Message;
            logger.LogError("Error Message: {exceptionMessage}, Time of occurrence {time}", exceptionMessage, DateTime.UtcNow);
            var error = exception.ToString();
            var message = exception.Message;
            var path = httpContext.Request.Path;
            var timestamp = DateTime.UtcNow;

            var status = exception switch
            {
                PlayerNotFoundException => 404,
                InvalidOperationException or 
                    ArgumentException or
                    ArgumentNullException or 
                    ArgumentOutOfRangeException or 
                    BadHttpRequestException => 400,
                _ => 500
            };

            httpContext.Response.StatusCode = status;
            httpContext.Response.ContentType = "application/json";
        
            var problemDetails = new ProblemDetails
            {
                Status = status,
                Title = message,
                Detail = error,
                Instance = path.ToString(),
                Type = exception.GetType().Name,
                Extensions =
                {
                    ["TimeStamp"] = timestamp,
                    ["Status"] = status,
                }

            };

            await httpContext.Response.WriteAsJsonAsync(problemDetails, ProblemDetailsGenerationContext.Default.ProblemDetails, "application/json", cancellationToken);
            await httpContext.Response.Body.FlushAsync(cancellationToken);
            return true;
        }
    }
}
