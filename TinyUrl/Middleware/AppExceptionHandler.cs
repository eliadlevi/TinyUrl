using Microsoft.AspNetCore.Diagnostics;
using TinyUrl.Exceptions;
using TinyUrl.Logger;

namespace TinyUrl.Middleware
{
    public class AppExceptionHandler : IExceptionHandler
    {
        private readonly ILog _logger;
        public AppExceptionHandler(ILog logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            (int statusCode, string errorMesssage) = exception switch
            {
                DBConnectionException dBConnectionException => (500, dBConnectionException.Message),
                UrlNotFoundException urlNotFoundException => (404, urlNotFoundException.Message),
                NotAValidUrlException notAValidUrlException => (404, notAValidUrlException.Message),
                _ => (500, "Something went wrong")
            };
            _logger.LogError($"Exception Catched by the AppExceptionHandler with statusCode: {statusCode} Exception: {exception.Message}");
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(errorMesssage);
            return true;
        }
    }
}
