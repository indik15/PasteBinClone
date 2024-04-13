using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Net;

namespace PasteBinClone.API.ExceptionHandler
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, 
            Exception exception, 
            CancellationToken cancellationToken)
        {
            Log.Error("An error occurred while processing the request: {Message}",
                exception.Message);

            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.NotFound,
                Type = exception.GetType().Name,
            };

            await httpContext
                .Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
