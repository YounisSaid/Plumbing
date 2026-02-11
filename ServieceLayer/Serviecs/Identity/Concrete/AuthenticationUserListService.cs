using AutoMapper;
using EntityLayer.Identity.Entites;
using EntityLayer.WebApplication.ViewModels.UserList;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using ServiceLayer.Messages.Identity;
using ServiceLayer.Serviecs.Identity.Abstract;
using System.Security.Claims;

namespace ServiceLayer.Serviecs.Identity.Concrete
{
    public class AuthenticationUserListService : IAuthenticationUserListService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public readonly IToastNotification _toasty;

        public AuthenticationUserListService(UserManager<AppUser> userManager, IMapper mapper, IToastNotification toasty)
        {
            _userManager = userManager;
            _mapper = mapper;
            _toasty = toasty;
        }

        public async Task ExtendClaimAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                _toasty.AddErrorToastMessage(NotificationMessagesIdentity.UserError, new ToastrOptions { Title = NotificationMessagesIdentity.FailedTitle });
                return;
            }

            var claims = await _userManager.GetClaimsAsync(user);
            var adminObserverClaim = claims.FirstOrDefault(c => c.Type.Contains("Observer"));

            if (Convert.ToDateTime(adminObserverClaim!.Value) > DateTime.Now)
            {
                _toasty.AddErrorToastMessage("User Already Have Valid Claim!!", new ToastrOptions { Title = NotificationMessagesIdentity.FailedTitle });
                return;
            }

            var newClaim = new Claim("AdminObserverExpireDate", DateTime.Now.AddDays(5).ToString());
            var replaceClaim = await _userManager.ReplaceClaimAsync(user, adminObserverClaim, newClaim);

            if (!replaceClaim.Succeeded)
            {
                _toasty.AddErrorToastMessage(NotificationMessagesIdentity.ExtendClaimFailed, new ToastrOptions { Title = NotificationMessagesIdentity.FailedTitle });
                return;
            }
            _toasty.AddSuccessToastMessage(NotificationMessagesIdentity.ExtendClaimSuccess, new ToastrOptions { Title = NotificationMessagesIdentity.SuccessedTitle });
        }

        public async Task<List<UserVM>> GetUserListAsync()
        {
            var userList = await _userManager.Users.ToListAsync();
            var mappedUserList = _mapper.Map<List<UserVM>>(userList);

            for (int i = 0; i < userList.Count(); i++)
            {
                mappedUserList[i].UserRoles = await _userManager.GetRolesAsync(userList[i]);
                mappedUserList[i].UserClaims = await _userManager.GetClaimsAsync(userList[i]);

            }
            return mappedUserList;
        }
    }
}
