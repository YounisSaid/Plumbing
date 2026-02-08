using EntityLayer.WebApplication.Entites;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.Filters.WebApplication;

namespace ServiceLayer.Extension.WebApplication
{
    public static class WebApplicationExtension
    {
        public static IServiceCollection LoadWebApplicationExtensions(this IServiceCollection services)
        {
            services.AddScoped(typeof(AddGenericPreventionFilter<About>));
            services.AddScoped(typeof(AddGenericPreventionFilter<Contact>));
            return services;
        }
    }
}
