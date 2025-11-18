using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.UnitOfWorks.Abstract;
using RepositoryLayer.UnitOfWorks.Concrete;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.Repositories.Concrete;
namespace RepositoryLayer.Extensions
{
    public static class RepositoryLayerExtension
    {
        public static IServiceCollection LoadRepositoryLayerExtensions(this IServiceCollection services,IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IUnitOfWork,UnitOfWork>();

            return services;
        }
    }
}
