using EntityLayer.Identity.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RepositoryLayer.Context;

namespace ServiceLayer.Extension.Identity
{
    public static class IdentityExtention
    {
        public static IServiceCollection LoadIdentityExtention(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
                {
                    options.Password.RequiredLength = 10;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequiredUniqueChars = 3;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(60);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                })
                .AddRoleManager<RoleManager<AppRole>>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();


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

            return services;
        }
    }
}
