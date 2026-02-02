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

        public async Task<IViewComponentResult> InvokeAsync(string Username)
        {
            if(Username is null)
            {
                Username = User.Identity!.Name!;
            }
            var user = await _userManager.FindByNameAsync(Username);

            if(user!.FileName is null)
            {
                return View(new UserPictureVM { FileName = "Default" });
            }
            return View(new UserPictureVM { FileName = $"{user.FileName}" });
        }
    }
}
