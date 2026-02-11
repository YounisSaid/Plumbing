using AutoMapper;
using EntityLayer.Identity.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using ServiceLayer.Serviecs.Identity.Abstract;

namespace Plumbing.MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    [Area("Admin")]
    public class UserListController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public readonly IToastNotification _toasty;
        private readonly IAuthenticationUserListService _authenticationUserListService;

        public UserListController(UserManager<AppUser> userManager, IMapper mapper, IToastNotification toasty, IAuthenticationUserListService authenticationUserListService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _toasty = toasty;
            _authenticationUserListService = authenticationUserListService;
        }

        public async Task<IActionResult> GetUserList()
        {
            var mappedUserList = await _authenticationUserListService.GetUserListAsync();
            return View(mappedUserList);
        }

        public async Task<IActionResult> ExtendClaim(string username)
        {
            await _authenticationUserListService.ExtendClaimAsync(username);
            return RedirectToAction("GetUserList", "UserList", new { Area = "Admin" });

        }
    }
}
