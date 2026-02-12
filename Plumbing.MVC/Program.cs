using NLog.Web;
using NToastNotify;
using RepositoryLayer.Extensions;
using ServiceLayer.Extension;
using ServiceLayer.Middlewares.Identity;
namespace Plumbing.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddNToastNotifyToastr(new ToastrOptions()
            {
                ProgressBar = true,
                PositionClass = ToastPositions.TopCenter
            });
            builder.Services.LoadRepositoryLayerExtensions(builder.Configuration);
            builder.Services.LoadServiceLayerExtensions(builder.Configuration);

            // NLog: Setup NLog for Dependency injection
            builder.Logging.ClearProviders();
            builder.Host.UseNLog();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStatusCodePagesWithReExecute("/Error/PageNotFound");
            app.UseExceptionHandler("/Error/GeneralExceptions");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthorization();
            app.UseMiddleware<SecurityStampCheck>();

#pragma warning disable ASP0014
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                name: "Admin",
                areaName: "Admin",
                pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                name: "User",
                areaName: "User",
                pattern: "User/{controller=Dashboard}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run();
        }
    }
}
