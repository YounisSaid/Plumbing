using EntityLayer.Identity.Entites;
using EntityLayer.Identity.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ServiceLayer.Serviecs.Identity.Abstract
{
    public interface IAuthenticationUserService
    {
        Task<UserEditMV> FindUserAsync(HttpContext context);
        Task<IdentityResult> UserEditAsync(UserEditMV userEditMV, AppUser user);
    }
}
