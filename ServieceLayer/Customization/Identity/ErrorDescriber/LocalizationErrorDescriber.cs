using Microsoft.AspNetCore.Identity;

namespace ServiceLayer.Customization.Identity.ErrorDescriber
{
    public class LocalizationErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordRequiresLower()
        {
            return new() {Code="NewLowerPasswordError" ,Description="Password Requires At Least one Lower Charcter !!" };
        }
        public override IdentityError PasswordTooShort(int length)
        {
            return new() { Code = "NewPasswordTooShortError", Description = "Password is To Short !!" };
        }
    }
}
