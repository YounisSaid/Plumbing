using AutoMapper;
using EntityLayer.Identity.Entites;
using EntityLayer.Identity.ViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Helpers.Identity.ModelStateHelper;
using ServiceLayer.Serviecs.Identity.Abstract;

namespace Plumbing.MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IValidator<SignUpVM> _signUpValidator;
        private readonly IValidator<LoginMV> _loginValidator;
        private readonly IValidator<ForgetPasswordMV> _ForgetPasswordValidator;
        private readonly IMapper _mapper;
        private readonly IValidator<ResetPasswordVM> _resetPasswordValidator;
        private readonly IAuthenticationCustomService _authenticationCustomService;

        public AuthenticationController(UserManager<AppUser> userManager,
                                        SignInManager<AppUser> signInManager,
                                        IValidator<SignUpVM> signUpValidator,
                                        IValidator<LoginMV> loginValidator,
                                        IValidator<ForgetPasswordMV> forgetPasswordValidator,
                                        IMapper mapper,
                                        IValidator<ResetPasswordVM> resetPasswordValidator,
                                        IAuthenticationCustomService authenticationCustomService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _signUpValidator = signUpValidator;
            _loginValidator = loginValidator;
            _ForgetPasswordValidator = forgetPasswordValidator;
            _mapper = mapper;
            _resetPasswordValidator = resetPasswordValidator;
            _authenticationCustomService = authenticationCustomService;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpVM input)
        {
            var validator = await _signUpValidator.ValidateAsync(input);
            if (!validator.IsValid)
            {

                validator.AddToModelState(ModelState);
                return View(input);
            }

            var user = _mapper.Map<AppUser>(input);
            var userCreatedResult = await _userManager.CreateAsync(user, input.Password);
            if (!userCreatedResult.Succeeded)
            {
                ViewBag.Result = "Failed";
                ModelState.AddModelStateListErrors(userCreatedResult.Errors);
                return View(input);
            }

            return RedirectToAction("Login", "Authentication");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Login(LoginMV input, string? returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Action("Index", "Dashboard", new { Area = "Admin" });

            var validator = await _loginValidator.ValidateAsync(input);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return View(input);
            }

            var user = await _userManager.FindByEmailAsync(input.Email);
            if (user == null)
            {
                ViewBag.Result = "Failed";
                ModelState.AddModelStateListErrors(new List<string> { "Email or Password Is Wrong!!" });
                return View(input);
            }

            var loginResult = await _signInManager.PasswordSignInAsync(user, input.Password, input.RememberMe, true);
            if (loginResult.Succeeded)
            {
                return Redirect(returnUrl!);
            }

            if (loginResult.IsLockedOut)
            {
                ViewBag.Result = "LockedOut";
                ModelState.AddModelStateListErrors(new List<string> { "Your Account is Locked Out For 60 Seconds !!" });
                return View(input);
            }

            ViewBag.Result = "FailedAttempt";
            ModelState.AddModelStateListErrors(new List<string> { $"Email or Password Is Wrong!! Failed Attempts : {await _userManager.GetAccessFailedCountAsync(user)} /5" });
            return View(input);
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordMV input)
        {
            var validator = await _ForgetPasswordValidator.ValidateAsync(input);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return View(input);
            }
            var user = await _userManager.FindByEmailAsync(input.Email);
            if (user == null)
            {
                ViewBag.Result = "UserNotFound";
                ModelState.AddModelStateListErrors(new List<string> { "User is Not Found!!" });
                return View(input);
            }

            await _authenticationCustomService.CreatePasswordCardentialsAndSend(user, HttpContext, input.Email, Url);

            return RedirectToAction("Login", "Authentication");
        }

        [HttpGet]
        public IActionResult ResetPassword(string UserId, string Token, List<string> errors)
        {
            TempData["Id"] = UserId;
            TempData["Token"] = Token;

            if (errors.Any())
            {
                ViewBag.Result = "Error";
                ModelState.AddModelStateListErrors(errors);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM input)
        {
            var token = TempData["Token"];
            var id = TempData["Id"];

            if (token == null || id == null)
            {
                // Invalid ID or TOKEN
                //Toaster Message Later
                return RedirectToAction("Login", "Authentication");
            }

            var validator = await _resetPasswordValidator.ValidateAsync(input);
            if (!validator.IsValid)
            {
                List<string> errors = validator.Errors.Select(x => x.ErrorMessage).ToList();
                return RedirectToAction("ResetPassword", "Authentication", new { UserId = id, Token = token, errors });
            }

            var user = await _userManager.FindByIdAsync(id!.ToString()!);
            if (user == null)
            {
                // Invalid ID or TOKEN
                //Toaster Message Later
                return RedirectToAction("Login", "Authentication");
            }

            var resetPasswordResult = await _userManager.ResetPasswordAsync(user!, token!.ToString()!, input.Password);
            if (resetPasswordResult.Succeeded)
            {
                //Toaster Message Later
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                List<string> errors = validator.Errors.Select(x => x.ErrorMessage).ToList();
                return RedirectToAction("ResetPassword", "Authentication", new { UserId = user.Id, Token = token, errors });
            }


        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
