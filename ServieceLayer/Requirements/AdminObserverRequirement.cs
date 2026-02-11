using EntityLayer.Identity.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ServiceLayer.Requirements
{
    public class AdminObserverRequirement : IAuthorizationRequirement
    {
    }

    public class AdminObserverRequirementHandler : AuthorizationHandler<AdminObserverRequirement>
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AdminObserverRequirementHandler(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminObserverRequirement requirement)
        {
            //Check if is in SuperAdminRole
            bool hasSuperAdminRole = context.User.IsInRole("SuperAdmin");
            if (hasSuperAdminRole)
            {
                context.Succeed(requirement);
                return;
            }

            //Check if cookieExpireDate is vaild
            var claim = context.User.Claims.First(x => x.Type.Contains("Observer"));
            if (claim == null)
            {
                context.Fail();
                return;
            }

            var cookieExpireDate = Convert.ToDateTime(claim.Value);
            if (cookieExpireDate > DateTime.Now)
            {
                context.Succeed(requirement);
                return;
            }
            //Check dbExpireDate and update cookieExpireDate is dbExpireDate vaild
            var user = await _userManager.FindByNameAsync(context.User.Identity!.Name!);
            var claims = await _userManager.GetClaimsAsync(user!);
            var dbExpireDate = Convert.ToDateTime(claims.First(x => x.Type.Contains("Observer")).Value);
            if (cookieExpireDate < dbExpireDate)
            {
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(user!, isPersistent: false);
                context.Succeed(requirement);
                return;
            }
            context.Fail();
            return;

        }
    }

}
