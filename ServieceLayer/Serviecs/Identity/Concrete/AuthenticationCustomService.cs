using EntityLayer.Identity.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Helpers.Identity.EmailHelper;
using ServiceLayer.Serviecs.Identity.Abstract;
using System;

namespace ServiceLayer.Serviecs.Identity.Concrete
{
    public class AuthenticationCustomService : IAuthenticationCustomService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSendMethodHelper _emailSendMethodHelper;

        public AuthenticationCustomService(IEmailSendMethodHelper emailSendMethodHelper, UserManager<AppUser> userManager)
        {
            _emailSendMethodHelper = emailSendMethodHelper;
            _userManager = userManager;
        }

        public async Task CreatePasswordCardentialsAndSend(AppUser user, HttpContext context, string email, IUrlHelper url)
        {
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var passwordResetLink = url.Action("ResetPassword", "Authentication", new { UserId = user.Id, Token = resetToken }, context.Request.Scheme);

            await _emailSendMethodHelper.SendPasswordResetLinkWithToken(passwordResetLink!, email);
        }
    }
}
