using Microsoft.Extensions.DependencyInjection;
using PruebaAnnarApi.Application.Mapper;
using PruebaAnnarApi.Application.Ports;
using PruebaAnnarApi.Application.Services;

namespace PruebaAnnarApi.Application.DependencyInjection
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            services.AddAutoMapper(typeof(GlobalMapperProfile));

            return services;
        }
    }
}
