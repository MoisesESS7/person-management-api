using Microsoft.AspNetCore.Mvc;
using Application.Exceptions;
using Domain.Exceptions;
using Shared.Exceptions;
using Shared.Utils;

namespace Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly IConfiguration _configuration;
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(IConfiguration configuration, RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _configuration = configuration;
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
            var traceId = Guid.NewGuid().ToString();
            
            var (statusCode, title, type) = MappingException(exception);

            // Creates the ProblemDetails object (RFC 7807)
            var problem = new ProblemDetails
            {
                Type = type,
                Title = title,
                Status = statusCode,
                Detail = exception.Message,
                Instance = context.Request.Path
            };

            // If it is a BaseAppException, add the specific errors.            
            if (exception is BaseAppException appException && appException.Errors.Count > 0)
            {
                problem.Extensions["errors"] = appException.Errors;
            }

            // Add traceId for correlation
            problem.Extensions["traceId"] = traceId;

            // Log structured
            if (statusCode == StatusCodes.Status500InternalServerError)
                _logger.LogError(exception, "Unhandled exception [{TraceId}]: {Message}", traceId, exception.Message);
            else
                _logger.LogWarning(exception, "Handled domain exception [{TraceId}]: {Message}", traceId, exception.Message);

            // Defines HTTP response
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = problem.Status ?? StatusCodes.Status500InternalServerError;

            var json = JsonHelper.Serialize(problem);
            
            await context.Response.WriteAsync(json);
        }

        private (int statusCode, string title, string type) MappingException(Exception exception)
        {
            return exception switch
            {
                InfrastructureLayerException infra => (infra.StatusCode, infra.Title, infra.Type),
                ApplicationLayerException app => (app.StatusCode, app.Title, app.Type),
                DomainLayerException domain => (domain.StatusCode, domain.Title, domain.Type),
                KeyNotFoundException => (StatusCodes.Status404NotFound, "Resource Not Found", _configuration["httpstatuses:400"]!),
                UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthorized", _configuration["httpstatuses:401"]!),
                _ => (StatusCodes.Status500InternalServerError, "Internal Server Error", _configuration["httpstatuses:500"]!)
            };
        }
    }
}
