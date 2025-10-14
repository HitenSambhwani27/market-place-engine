using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Exceptions.Handlers
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

            (string detail, string title, int status) problemDetails = exception switch
            {
               InternalServerException => (exception.Message, "Internal Server Error", StatusCodes.Status500InternalServerError),
                ValidationException => (exception.Message, "Validation Error", StatusCodes.Status400BadRequest),
                BadRequestException => (exception.Message, "Bad Request", StatusCodes.Status400BadRequest),
                _ => (exception.StackTrace ?? string.Empty, "An unexpected error occurred.", StatusCodes.Status500InternalServerError)
            };
            var problem = new ProblemDetails
            {
                Title = problemDetails.title,
                Detail = problemDetails.detail,
                Status = problemDetails.status
            };

            await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);
            return true;
        }
    }
}
