using AutoMapper;
using EntityLayer.Identity.Entites;
using EntityLayer.WebApplication.ViewModels.UserList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using ServiceLayer.Messages.Identity;
using System.Security.Claims;

namespace Plumbing.MVC.Areas.Admin.Controllers
{
    [Authorize(Policy = "AdminObserver")]
    [Area("Admin")]
    public class UserListController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public readonly IToastNotification _toasty;

        public UserListController(UserManager<AppUser> userManager, IMapper mapper, IToastNotification toasty)
        {
            _userManager = userManager;
            _mapper = mapper;
            _toasty = toasty;
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

        public async Task<IActionResult> ExtendClaim(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                _toasty.AddErrorToastMessage(NotificationMessagesIdentity.UserError, new ToastrOptions { Title = NotificationMessagesIdentity.FailedTitle });
                return RedirectToAction("GetUserList", "UserList", new { Area = "Admin" });
            }

            var claims = await _userManager.GetClaimsAsync(user);
            var adminObserverClaim = claims.FirstOrDefault(c => c.Type.Contains("Observer"));

            if (Convert.ToDateTime(adminObserverClaim!.Value) > DateTime.Now)
            {
                _toasty.AddErrorToastMessage("User Already Have Valid Claim!!", new ToastrOptions { Title = NotificationMessagesIdentity.FailedTitle });
                return RedirectToAction("GetUserList", "UserList", new { Area = "Admin" });
            }

            var newClaim = new Claim("AdminObserverExpireDate", DateTime.Now.AddDays(5).ToString());
            var replaceClaim = await _userManager.ReplaceClaimAsync(user, adminObserverClaim, newClaim);

            if (!replaceClaim.Succeeded)
            {
                _toasty.AddErrorToastMessage(NotificationMessagesIdentity.ExtendClaimFailed, new ToastrOptions { Title = NotificationMessagesIdentity.FailedTitle });
                return RedirectToAction("GetUserList", "UserList", new { Area = "Admin" });
            }
            _toasty.AddSuccessToastMessage(NotificationMessagesIdentity.ExtendClaimSuccess, new ToastrOptions { Title = NotificationMessagesIdentity.SuccessedTitle });
            return RedirectToAction("GetUserList", "UserList", new { Area = "Admin" });

        }
    }
}
