using AutoMapper;
using EntityLayer.Enumerates;
using EntityLayer.Identity.Entites;
using EntityLayer.Identity.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ServiceLayer.Helpers.Generic;
using ServiceLayer.Serviecs.Identity.Abstract;

namespace ServiceLayer.Serviecs.Identity.Concrete
{
    public class AuthenticationUserService : IAuthenticationUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IImageHelper _imageHelper;

        public AuthenticationUserService(UserManager<AppUser> userManager, IMapper mapper, SignInManager<AppUser> signInManager, IImageHelper imageHelper)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _imageHelper = imageHelper;
        }

        public async Task<UserEditMV> FindUserAsync(HttpContext context)
        {
            var user = await _userManager.FindByNameAsync(context.User.Identity!.Name!);
            var userEditMv = _mapper.Map<UserEditMV>(user);
            return userEditMv;
        }

        public async Task<IdentityResult> UserEditAsync(UserEditMV input, AppUser user)
        {
            var checkPassword = await _userManager.CheckPasswordAsync(user!, input.Password);
            if (!checkPassword)
            {

                var errors = new IdentityError() { Code = "WrongPasswordError", Description = "Password is Wrong!!!" };
                var passwordFail = IdentityResult.Failed(errors);
            }
            if (input.NewPassword != null)
            {
                var PasswordChange = await _userManager.ChangePasswordAsync(user!, input.Password, input.NewPassword!);
                if (!PasswordChange.Succeeded)
                {
                    return PasswordChange;
                }
            }
            var oldFileName = user!.FileName;
            var oldFileType = user.FileType;

            if (input.Photo != null)
            {
                var image = await _imageHelper.UploadImageAsync(null, input.Photo, imageType.identity);
                if (image.Error != null)
                {
                    if (input.NewPassword != null)
                    {
                        await _userManager.ChangePasswordAsync(user, input.NewPassword, input.Password!);
                        await _userManager.UpdateSecurityStampAsync(user);
                        await _signInManager.SignOutAsync();
                        await _signInManager.SignInAsync(user, isPersistent: false);
                    }
                    var errors = new IdentityError() { Code = "ImageError", Description = "Photo must be in GPG or GPEG or PNG" };
                    var passwordFail = IdentityResult.Failed(errors);
                }
                input.FileName = image.FileName;
                input.FileType = image.FileType;
            }
            else
            {
                input.FileName = oldFileName;
                input.FileType = oldFileType;
            }

            _mapper.Map(input, user);
            var userUpdate = await _userManager.UpdateAsync(user);
            if (userUpdate.Succeeded)
            {
                if (input.Photo != null)
                {
                    if (oldFileName != null)
                    {
                        _imageHelper.DeleteImage(oldFileName);
                    }
                }
                await _userManager.UpdateSecurityStampAsync(user);
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(user, isPersistent: false);
                return userUpdate;
            }

            if (input.FileName != null)
            {
                _imageHelper.DeleteImage(input.FileName);
            }
            if (input.NewPassword != null)
            {
                await _userManager.ChangePasswordAsync(user, input.NewPassword, input.Password!);
                await _userManager.UpdateSecurityStampAsync(user);
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(user, isPersistent: false);
            }
            return userUpdate;

        }
    }
}
