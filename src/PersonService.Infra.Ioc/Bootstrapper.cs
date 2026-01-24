using PersonService.Application.Interfaces.Repositories;
using PersonService.Infra.Data.Common;
using PersonService.Infra.Data.Context;
using PersonService.Infra.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PersonService.Infra.Ioc
{
    public static class Bootstrapper
    {
        public static void AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddSingleton<IMongoDbContext, MongoDbContext>();
            service.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            service.AddScoped<IRepositoryExecutor, RepositoryExecutor>();
        }
    }
}
