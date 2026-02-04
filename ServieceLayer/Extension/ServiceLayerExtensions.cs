
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.Extension.Identity;
using ServiceLayer.FluentValidation.WebApplication.HomePageValidation;
using ServiceLayer.Helpers.Generic;
using ServiceLayer.Serviecs.Identity.Abstract;
using ServiceLayer.Serviecs.Identity.Concrete;
using ServiceLayer.Serviecs.WebApplication.Abstract;
using ServiceLayer.Serviecs.WebApplication.Concrete;
using System.Reflection;

namespace ServiceLayer.Extension
{
    public static class ServiceLayerExtensions
    {
        public static IServiceCollection LoadServiceLayerExtensions(this IServiceCollection services,IConfiguration configuration)
        {
            services.LoadIdentityExtention(configuration);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

           

            // Register Services With Scrutor
            services.Scan(scan => scan
                                .FromAssemblyOf<AboutService>()
                                .AddClasses()
                                .AsMatchingInterface()
                                .WithScopedLifetime());


        


            services.AddFluentValidationAutoValidation(opt => 
                                                   opt.DisableDataAnnotationsValidation = true);

            services.AddValidatorsFromAssemblyContaining<HomePageAddValidation>();
            services.AddScoped<IImageHelper, ImageHelper>();


            return services;
        }
    }
}
