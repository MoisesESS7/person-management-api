using Application.Common.Validations.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class Bootstrapper
    {
        public static void AddApplication(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(Bootstrapper).Assembly));

            service.AddValidatorsFromAssembly(typeof(Bootstrapper).Assembly);

            service.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}
