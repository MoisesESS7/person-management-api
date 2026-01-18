using Api.Builders;
using Api.Mappers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.OpenApi.Models;

namespace Api
{
    public static class Bootstrapper
    {
        public static void AddApi(this IServiceCollection service, IConfiguration configuration)
        {
            // AutoMapper
            service.AddAutoMapper(c =>
            {
                c.AddProfile(typeof(PersonProfile));
            });

            // LinkBuiler
            service.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            service.AddScoped<ILinkBuilder, LinkBuilder>();

            // Swagger
            ConfigureSwagger(service, configuration);
        }

        private static void ConfigureSwagger(IServiceCollection service, IConfiguration configuration)
        {
            service.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PDI - Implementing CRUD operations with MongoDB and creating GCP PUB/SUB",
                    Version = "v1",
                    Description = "Implement CRUD operations with MongoDB by creating indexes and aggregation, best practices with S.O.L.I.D (SRP, OCP), and GCP PUB/SUB.",
                    Contact = new OpenApiContact
                    {
                        Name = "My name goes here - .NET Developer",
                        Email = "exemple@outlook.com",
                        Url = TryCreateUri(configuration["ContactUrl"])
                    }
                });

                // This groups controllers using EndpointGroupName
                options.TagActionsBy(api =>
                {
                    var groupName = api.GroupName;
                    return [groupName ?? api.ActionDescriptor.RouteValues["controller"]];
                });
                options.DocInclusionPredicate((name, api) => { return true; });
            });
        }

        private static Uri? TryCreateUri(string? url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return null;

            return Uri.TryCreate(url, UriKind.Absolute, out var uri) ? uri : null;
        }
    }
}
