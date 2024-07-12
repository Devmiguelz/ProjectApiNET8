using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PruebaAnnarApi.Domain.Interfaces;
using PruebaAnnarApi.Domain.Ports;
using PruebaAnnarApi.Infrastructure.Adapters;
using PruebaAnnarApi.Infrastructure.Persistence;

namespace PruebaAnnarApi.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
