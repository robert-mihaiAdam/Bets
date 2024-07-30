using Microsoft.AspNetCore.Diagnostics;
using Domain.ErrorEntities;

namespace BetsApi.Infrastructure
{
    public class GlobalErrorHandler : IExceptionHandler
    {
        private readonly ILogger _logger;

        public GlobalErrorHandler(ILogger<GlobalErrorHandler> logger)
        {
            _logger = logger;
        }
        
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, $"Unhandled Exception: {exception.Message}");

            switch(exception)
            {
                case NotFoundException _:
                    httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                    break;
                default:
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            ErrorResponse response = new ErrorResponse
            {
                message = exception.Message
            };
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
            return true;
        }
    }
}
