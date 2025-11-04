using Application.Interfaces.Repositories;
using Infrastructure.Data.Common;
using Infrastructure.Data.Data.Context;
using Infrastructure.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Ioc
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
