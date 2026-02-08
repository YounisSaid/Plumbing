using EntityLayer.Identity.Entites;
using Microsoft.AspNetCore.Identity;

namespace ServiceLayer.Customization.Identity.Validators
{
    public class CustomPasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string? password)
        {
            var errors = new List<IdentityError>();

            if(password!.ToLower().Contains(user!.UserName!.ToLower()))
            {
                errors.Add(new() {Code ="PasswordContainsUserNameError",Description="Password Can't Contain Username" });
            }
            if (password.StartsWith("1234"))
            {
                errors.Add(new() { Code = "PasswordStartsWith1234Error", Description = "Password Can't Start With 1234" });
            }
            if(errors.Any())
            {
               return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}
