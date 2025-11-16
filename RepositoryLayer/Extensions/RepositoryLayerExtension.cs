using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using Microsoft.EntityFrameworkCore;
namespace RepositoryLayer.Extensions
{
    public static class RepositoryLayerExtension
    {
        public static IServiceCollection LoadRepositoryLayerExtensions(this IServiceCollection services,IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
