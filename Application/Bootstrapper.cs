using Application.Interfaces.Services;
using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class Bootstrapper
    {
        public static void AddApplication(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddScoped<IPersonService, PersonService>();
        }
    }
}
