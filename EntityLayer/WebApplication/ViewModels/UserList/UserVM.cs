using System.Security.Claims;

namespace EntityLayer.WebApplication.ViewModels.UserList
{
    public class UserVM
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public IList<string> UserRoles = null!;

        public IList<Claim>? UserClaims;
    }
}
