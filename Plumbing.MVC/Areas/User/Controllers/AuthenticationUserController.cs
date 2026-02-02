using AutoMapper;
using EntityLayer.Identity.Entites;
using EntityLayer.Identity.ViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ServiceLayer.Helpers.Identity.ModelStateHelper;

namespace Plumbing.MVC.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class AuthenticationUserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IValidator<UserEditMV> _userEditValidator;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthenticationUserController(UserManager<AppUser> userManager, IMapper mapper, IValidator<UserEditMV> userEditValidator,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userEditValidator = userEditValidator;
            _signInManager = signInManager;
        }
        [HttpGet]
        public async Task<IActionResult> UserEdit()
        {
            var user = await _userManager.FindByNameAsync(User.Identity!.Name!);
            var userEditMv = _mapper.Map<UserEditMV>(user);
            return View(userEditMv);
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(UserEditMV input)
        {
            var user = await _userManager.FindByNameAsync(User.Identity!.Name!);
            var validator = await _userEditValidator.ValidateAsync(input);
            if(!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return View(input);
            }

            var checkPassword = await _userManager.CheckPasswordAsync(user!,input.Password);
            if(!checkPassword)
            {
                ViewBag.Result = "WrongPassword";
                ModelState.AddModelStateListErrors(new List<string>() { "Password is Wrong!!!" });
                return View(input);
            }
            if(input.NewPassword != null)
            {
                var PasswordChange = await _userManager.ChangePasswordAsync(user!,input.Password,input.NewPassword!);
                if(!PasswordChange.Succeeded)
                {
                    ViewBag.Result = "NewPasswordFailed";
                    ModelState.AddModelStateListErrors(PasswordChange.Errors);
                    return View(input);
                }
            }
            var oldFileName = user!.FileName;
            var oldFileType = user.FileType;

            if(input.Photo != null)
            {
                input.FileName = DateTime.Now.ToString();
                input.FileType = DateTime.Now.ToString();
            }
            else
            {
                input.FileName = oldFileName;
                input.FileType = oldFileType;
            }

            var mappedUser = _mapper.Map(input, user);
            var userUpdate = await _userManager.UpdateAsync(mappedUser);
            if(userUpdate.Succeeded)
            {
                if(input.Photo != null)
                {
                    if(oldFileName !=null)
                    {
                        //Delete Photo Logic
                    }
                }
                await _userManager.UpdateSecurityStampAsync(user);
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Dashboard", new { Area = "User" });
            }

            if(input.FileName != null)
            {
                //Delete New Photo if Failed
            }
            if (input.NewPassword != null)
            {
                await _userManager.ChangePasswordAsync(user, input.NewPassword, input.Password!);
                await _userManager.UpdateSecurityStampAsync(user);
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(user, isPersistent: false);
            }
            ViewBag.Username = user.UserName;
            return View();
        }



        }
    }

