using EntityLayer.Identity.Entites;
using EntityLayer.Identity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepositoryLayer.Context;
using ServiceLayer.Customization.Identity.ErrorDescriber;
using ServiceLayer.Customization.Identity.Validators;
using ServiceLayer.Helpers.Identity.EmailHelper;
using ServiceLayer.Requirements;

namespace ServiceLayer.Extension.Identity
{
    public static class IdentityExtention
    {
        public static IServiceCollection LoadIdentityExtention(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
                {
                    options.Password.RequiredLength = 10;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequiredUniqueChars = 3;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(60);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.User.RequireUniqueEmail = true;

                })
                .AddRoleManager<RoleManager<AppRole>>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<LocalizationErrorDescriber>()
                .AddPasswordValidator<CustomPasswordValidator>()
                .AddUserValidator<CustomUserValidator>();


            services.ConfigureApplicationCookie(opt =>
            {
                var newCookie = new CookieBuilder();
                newCookie.Name = "PlumbingCompany";
                opt.Cookie = newCookie;
                opt.LoginPath = new PathString("/Authentication/Login");
                opt.LogoutPath = new PathString("/Authentication/LogOut");
                opt.AccessDeniedPath = new PathString("/Authentication/AccessDenied");
                opt.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            });
            services.AddScoped<IEmailSendMethodHelper, EmailSendMethodHelper>();

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                                                                   opt.TokenLifespan = TimeSpan.FromMinutes(60));
            services.Configure<GmailInformationVM>(configuration.GetSection("EmailSettings"));

            services.AddScoped<IAuthorizationHandler, AdminObserverRequirementHandler>();

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("AdminObserver", policy =>
                {
                    policy.AddRequirements(new AdminObserverRequirement());
                });
            });

            return services;
        }
    }
}
