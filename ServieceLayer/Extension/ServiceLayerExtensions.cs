
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.Extension.Identity;
using ServiceLayer.FluentValidation.WebApplication.HomePageValidation;
using System.Reflection;

namespace ServiceLayer.Extension
{
    public static class ServiceLayerExtensions
    {
        public static IServiceCollection LoadServiceLayerExtensions(this IServiceCollection services,IConfiguration configuration)
        {
            services.LoadIdentityExtention(configuration);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());


            var types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service"));
            foreach (var type in types)
            {
                var interfaceType = type.GetInterfaces().FirstOrDefault(x => x.Name == $"I{type.Name}");
                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, type);
                }
            }

            services.AddFluentValidationAutoValidation(opt => 
                                                   opt.DisableDataAnnotationsValidation = true);

            services.AddValidatorsFromAssemblyContaining<HomePageAddValidation>();
            return services;
        }
    }
}
