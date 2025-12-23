using Microsoft.AspNetCore.Mvc;
using Shared.Results;

namespace Api.Extensions;

public static class ResultExtensions
{
    public static ActionResult ToActionResult<T>(this ResultOfT<T> result)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result.Value);

        return MapFailure(result.Errors);
    }

    public static ActionResult ToActionResult(this Result result)
    {
        if (result.IsSuccess)
            return new NoContentResult();

        return MapFailure(result.Errors);
    }

    private static ObjectResult MapFailure(IReadOnlyCollection<Error> errors)
    {
        var type = ResolveErrorType(errors);

        var status = type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError
        };

        var problemDetails = new ProblemDetails
        {
            Status = status,
            Title = type.ToString(),
            Type = $"https://httpstatuses.com/{status}",
        };
        
        problemDetails.Extensions["errors"] = errors;

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
