
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ServieceLayer.Extension
{
    public static class ServiceLayerExtensions
    {
        public static IServiceCollection LoadServiceLayerExtensions(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

                return services;
        }
    }
}
