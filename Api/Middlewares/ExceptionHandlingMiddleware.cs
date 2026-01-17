using Microsoft.AspNetCore.Mvc;
using Shared.Exceptions;
using Api.Utils;
using System.Diagnostics;
using FluentValidation;

namespace Api.Middlewares
{
    public sealed class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var traceId = Activity.Current?.Id ?? context.TraceIdentifier;

            ProblemDetails problem;

            if (exception is ValidationException validationException)
            {
                var errors = validationException.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogWarning("Validation error [{TraceId}] - {Errors}", traceId, errors);

                problem = CreateProblemDetails(
                    type: "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                    title: "Validation Error",
                    detail: "One or more validation errors occurred.",
                    statusCode: StatusCodes.Status400BadRequest,
                    instance: context.Request.Path,
                    errors: errors
                );
            }
            else if (exception is BusinessException businessException)
            {
                _logger.LogWarning("Handled exception [{TraceId}] - {Errors}", traceId, businessException.Errors);
                problem = CreateProblemDetails(
                    businessException.Type,
                    businessException.Title,
                    businessException.Message,
                    businessException.StatusCode,
                    context.Request.Path,
                    businessException.Errors
                );
            }
            else if (exception is TechnicalException technicalException)
            {
                _logger.LogError(exception, "Technical error [{TraceId}]", traceId);
                problem = CreateProblemDetails(
                    technicalException.Type,
                    technicalException.Title,
                    technicalException.Message,
                    technicalException.StatusCode,
                    context.Request.Path
                );
            }
            else
            {
                _logger.LogError(exception, "Unhandled exception [{TraceId}]", traceId);
                problem = CreateProblemDetails(
                    "https://httpstatuses.com/500",
                    "Internal Server Error",
                    "An unexpected error occurred.",
                    StatusCodes.Status500InternalServerError,
                    context.Request.Path
                );
            }

            // Add traceId for correlation
            problem.Extensions["traceId"] = traceId;

            // Defines HTTP response
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = problem.Status!.Value;

            var json = JsonHelper.Serialize(problem);

            await context.Response.WriteAsync(json);
        }

        private static ProblemDetails CreateProblemDetails(
            string type,
            string title,
            string detail,
            int statusCode,
            string instance,
            IReadOnlyCollection<string>? errors = null)
        {
            var problem = new ProblemDetails
            {
                Type = type,
                Title = title,
                Detail = detail,
                Status = statusCode,
                Instance = instance
            };

            if (errors?.Count > 0 )
            {
                problem.Extensions["errors"] = errors;
            }

            return problem;
        }
    }
}
