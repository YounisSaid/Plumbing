using AutoMapper;
using EntityLayer.Identity.Entites;
using EntityLayer.WebApplication.ViewModels.UserList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Plumbing.MVC.Areas.Admin.Controllers
{
    [Authorize(Policy = "AdminObserver")]
    [Area("Admin")]
    public class UserListController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserListController(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetUserList()
        {
            var userList = await _userManager.Users.ToListAsync();
            var mappedUserList = _mapper.Map<List<UserVM>>(userList);

            for (int i = 0; i < userList.Count(); i++)
            {
                mappedUserList[i].UserRoles = await _userManager.GetRolesAsync(userList[i]);
                mappedUserList[i].UserClaims = await _userManager.GetClaimsAsync(userList[i]);

            }
            return View(mappedUserList);
        }
    }
}
