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
            services.AddScoped(typeof(AddGenericPreventionFilter<HomePage>));

            services.AddScoped(typeof(GenericNotFoundFilter<About>));
            services.AddScoped(typeof(GenericNotFoundFilter<Category>));
            services.AddScoped(typeof(GenericNotFoundFilter<Contact>));
            services.AddScoped(typeof(GenericNotFoundFilter<HomePage>));
            services.AddScoped(typeof(GenericNotFoundFilter<Portfolio>));
            services.AddScoped(typeof(GenericNotFoundFilter<Service>));
            services.AddScoped(typeof(GenericNotFoundFilter<Team>));
            services.AddScoped(typeof(GenericNotFoundFilter<Testimonial>));


            return services;
        }
    }
}
