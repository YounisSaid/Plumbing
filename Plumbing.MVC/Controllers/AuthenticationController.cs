using AutoMapper;
using EntityLayer.Identity.Entites;
using EntityLayer.Identity.ViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using ServiceLayer.Helpers.Identity.EmailHelper;
using ServiceLayer.Helpers.Identity.ModelStateHelper;
using System.Threading.Tasks;

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
        private readonly IEmailSendMethodHelper _emailSendMethodHelper;

        public AuthenticationController(UserManager<AppUser> userManager,
                                        SignInManager<AppUser> signInManager, 
                                        IValidator<SignUpVM> signUpValidator,
                                        IValidator<LoginMV> loginValidator, 
                                        IValidator<ForgetPasswordMV> forgetPasswordValidator,
                                        IMapper mapper,
                                        IEmailSendMethodHelper emailSendMethodHelper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _signUpValidator = signUpValidator;
            _loginValidator = loginValidator;
            _ForgetPasswordValidator = forgetPasswordValidator;
            _mapper = mapper;
            _emailSendMethodHelper = emailSendMethodHelper;
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
            if(!validator.IsValid)
            {
                
                validator.AddToModelState(ModelState);
                return View(input);
            }

            var user = _mapper.Map<AppUser>(input);
            var userCreatedResult = await _userManager.CreateAsync(user,input.Password);
            if(!userCreatedResult.Succeeded)
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
            returnUrl =  returnUrl ?? Url.Action("Index","Dashboard",new {Area = "Admin"});

            var validator = await _loginValidator.ValidateAsync(input);
            if(!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return View(input);
            }

            var user = await _userManager.FindByEmailAsync(input.Email);
            if(user == null)
            {
                ViewBag.Result = "Failed";
                ModelState.AddModelStateListErrors(new List<string>{"Email or Password Is Wrong!!"});
                return View(input);
            }

            var loginResult = await _signInManager.PasswordSignInAsync(user, input.Password, input.RememberMe, true);
            if(loginResult.Succeeded)
            {
                return Redirect(returnUrl!);
            }

            if(loginResult.IsLockedOut)
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
            if(!validator.IsValid)
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
           
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var passwordResetLink = Url.Action("ResetPassword", "Authentication", new {UserId = user.Id,Token = resetToken,HttpContext.Request.Scheme});

            await _emailSendMethodHelper.SendPasswordResetLinkWithToken(passwordResetLink!, input.Email);

            return RedirectToAction("Login", "Authentication");
        }
    }
}
