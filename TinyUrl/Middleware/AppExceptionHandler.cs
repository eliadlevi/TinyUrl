using Microsoft.AspNetCore.Diagnostics;
using TinyUrl.Exceptions;

namespace TinyUrl.Middleware
{
    public class AppExceptionHandler : IExceptionHandler
    {
        private readonly ILogger _logger;
        public AppExceptionHandler(ILogger<AppExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            (int statusCode, string errorMesssage) = exception switch
            {
                DBConnectionException dBConnectionException => (500, dBConnectionException.Message),
                UrlNotFoundException urlNotFoundException => (404, urlNotFoundException.Message),
                _ => (500, "Something went wrong")
            };
            _logger.LogError(statusCode, exception.Message);
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(errorMesssage);
            return true;
        }
    }
}
