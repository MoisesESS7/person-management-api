using Microsoft.AspNetCore.Mvc;
using PersonService.Shared.Results;
using System.Diagnostics;

namespace PersonService.Api.Extensions;

public static class ControllerExtensions
{
    public static ActionResult Map<T>(this ControllerBase controller, ResultOfT<T> result)
    {
        if (result.IsSuccess)
            return controller.Ok(result.Value);

        return MapFailure(controller, result.Errors);
    }

    public static ActionResult Map(this ControllerBase controller, Result result)
    {
        if (result.IsSuccess)
            return controller.NoContent();

        return MapFailure(controller, result.Errors);
    }

    private static ObjectResult MapFailure(ControllerBase controller, IReadOnlyCollection<Error> errors)
    {
        var type = ResolveErrorType(errors);

        var (status, title) = type switch
        {
            ErrorType.Validation => (StatusCodes.Status400BadRequest, "Validation error ocurred"),
            ErrorType.NotFound => (StatusCodes.Status404NotFound, "Resource not found"),
            ErrorType.Conflict => (StatusCodes.Status409Conflict, "Conflict error"),
            ErrorType.Unauthorized => (StatusCodes.Status401Unauthorized, "Unauthorized"),
            ErrorType.Forbidden => (StatusCodes.Status403Forbidden, "Forbidden"),
            _ => (StatusCodes.Status500InternalServerError,  "Unhandled error ocurred")
        };

        var problemDetails = new ProblemDetails
        {
            Status = status,
            Title = title,
            Type = $"https://httpstatuses.com/{status}",
            Instance = controller.HttpContext.Request.Path
        };
        
        problemDetails.Extensions["errors"] = errors;
        problemDetails.Extensions["traceId"] = Activity.Current?.Id ?? controller.HttpContext.TraceIdentifier;

        return new ObjectResult(problemDetails)
        {
            StatusCode = status,
            ContentTypes = { "application/problem+json" }
        };
    }

    private static ErrorType ResolveErrorType(IReadOnlyCollection<Error> errors)
    {
        if (errors.Count == 0)
            return ErrorType.Failure;

        var firstType = errors.First().Type;

        if (errors.Any(e => e.Type != firstType))
            return ErrorType.Failure;

        return firstType;
    }
}
