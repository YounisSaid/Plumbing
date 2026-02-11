using EntityLayer.Identity.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ServiceLayer.Messages.Identity;

namespace ServiceLayer.Middlewares.Identity
{
    public class SecurityStampCheck
    {
        private readonly RequestDelegate _next;

        public SecurityStampCheck(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context, UserManager<AppUser> userManager)
        {
            if (context.User.Identity == null)
            {
                await _next.Invoke(context);
                return;
            }

            if (context.User.Identity!.IsAuthenticated)
            {
                var user = await userManager.GetUserAsync(context.User);
                var cookieSecurityStamp = context.User.Claims.FirstOrDefault(c => c.Type.Contains("SecurityStamp"))!.Value;
                if (cookieSecurityStamp != user!.SecurityStamp)
                {
                    context.Response.Cookies.Delete("PlumbingCompany");
                    context.Response.Redirect($"/Authentication/Login?errorMessage={IdentityValidationMessages.SecurityStampError}");
                    return;
                }
            }
            await _next.Invoke(context);
            return;
        }
    }
}
