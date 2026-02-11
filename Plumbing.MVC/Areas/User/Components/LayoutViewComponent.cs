using EntityLayer.Identity.Entites;
using EntityLayer.Identity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Plumbing.MVC.Areas.User.Components
{
    [Authorize]
    [Area("User")]
    public class LayoutViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public LayoutViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            if (id is null)
            {
                id = UserClaimsPrincipal.Claims.FirstOrDefault(c => c.Type.Contains("identifier"))!.Value;
            }
            var user = await _userManager.FindByIdAsync(id);

            if (user!.FileName is null)
            {
                return View(new UserPictureVM { FileName = "Default" });
            }
            return View(new UserPictureVM { FileName = $"{user.FileName}" });
        }
    }
}
