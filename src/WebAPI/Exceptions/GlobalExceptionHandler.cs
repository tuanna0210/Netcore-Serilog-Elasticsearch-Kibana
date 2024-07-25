using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Exceptions;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger,
    IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError("Error Message: {exceptionMessage}, Time of occurence {time}", exception.Message, DateTime.UtcNow);
        //(string Detail, string Title, int StatusCode) details = exception switch
        //{
        //    InternalServerException =>
        //    (
        //        exception.Message,
        //        exception.GetType().Name,
        //        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError
        //    ),
        //    ValidationException =>
        //    (
        //        exception.Message,
        //        exception.GetType().Name,
        //        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
        //    ),
        //    BadRequestException =>
        //    (
        //        exception.Message,
        //        exception.GetType().Name,
        //        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
        //    ),
        //    NotFoundException =>
        //    (
        //        exception.Message,
        //        exception.GetType().Name,
        //        httpContext.Response.StatusCode = StatusCodes.Status404NotFound
        //    ),
        //    _ =>
        //    (
        //        exception.Message,
        //        exception.GetType().Name,
        //        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError
        //    )
        //};

        //var problemDetails = new ProblemDetails
        //{
        //    Title = details.Title,
        //    Detail = details.Detail,
        //    Status = details.StatusCode,
        //    Instance = httpContext.Request.Path
        //};

        //problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);

        //if (exception is ValidationException validationException)
        //{
        //    problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);
        //}

        var problemDetails = new ProblemDetails
        {
            Title = "details.Title",
            Detail = "details.Detail",
            Status = StatusCodes.Status500InternalServerError,
            Instance = httpContext.Request.Path
        };

        problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext()
        {
            HttpContext = httpContext,
            ProblemDetails = problemDetails,
            Exception = exception,
        });
    }
}