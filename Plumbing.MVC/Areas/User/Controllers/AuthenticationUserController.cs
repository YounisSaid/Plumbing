using EntityLayer.Identity.Entites;
using EntityLayer.Identity.ViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using ServiceLayer.Helpers.Identity.ModelStateHelper;
using ServiceLayer.Messages.Identity;
using ServiceLayer.Serviecs.Identity.Abstract;

namespace Plumbing.MVC.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class AuthenticationUserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IValidator<UserEditMV> _userEditValidator;
        private readonly IAuthenticationUserService _authenticationUserService;
        private readonly IToastNotification _toasty;

        public AuthenticationUserController(UserManager<AppUser> userManager,
            IValidator<UserEditMV> userEditValidator,
            IAuthenticationUserService authenticationUserService,
            IToastNotification toasty)
        {
            _userManager = userManager;
            _userEditValidator = userEditValidator;
            _authenticationUserService = authenticationUserService;
            _toasty = toasty;
        }

        [HttpGet]
        public async Task<IActionResult> UserEdit()
        {
            var userEditVM = await _authenticationUserService.FindUserAsync(HttpContext);
            return View(userEditVM);
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(UserEditMV input)
        {
            var user = await _userManager.FindByNameAsync(User.Identity!.Name!);
            var validator = await _userEditValidator.ValidateAsync(input);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return View();
            }

            var userEditResult = await _authenticationUserService.UserEditAsync(input, user!);
            if (!userEditResult.Succeeded)
            {
                ViewBag.Result = "UserEditFailed";
                ModelState.AddModelStateListErrors(userEditResult.Errors);
                return View();
            }
            ViewBag.Username = user!.UserName;
            _toasty.AddInfoToastMessage(NotificationMessagesIdentity.UserEdit(user.UserName!), new ToastrOptions { Title = NotificationMessagesIdentity.SuccessedTitle });
            return RedirectToAction("Index", "Dashboard", new { Area = "User" });

        }



    }
}

