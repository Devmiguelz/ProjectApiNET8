using Microsoft.Extensions.DependencyInjection;
using PruebaAnnarApi.Domain.Ports;
using PruebaAnnarApi.Domain.Services;

namespace PruebaAnnarApi.Domain.DependencyInjection
{
    public static class DomainServiceExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
