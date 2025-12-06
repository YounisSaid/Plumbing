using AutoMapper;
using EntityLayer.Identity.Entites;
using EntityLayer.Identity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Plumbing.MVC.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class AuthenticationUserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AuthenticationUserController(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> UserEdit()
        {
            var user = await _userManager.FindByNameAsync(User.Identity!.Name!);
            var userEditMv = _mapper.Map<UserEditMV>(user);
            return View(userEditMv);
        }
    }
}
