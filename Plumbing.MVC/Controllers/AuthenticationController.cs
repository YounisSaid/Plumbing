using AutoMapper;
using EntityLayer.Identity.Entites;
using EntityLayer.Identity.ViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Helpers.Identity;

namespace Plumbing.MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IValidator<SignUpVM> _validator;
        private readonly IMapper _mapper;

        public AuthenticationController(UserManager<AppUser> userManager, IValidator<SignUpVM> validator, IMapper mapper)
        {
            _userManager = userManager;
            _validator = validator;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpVM input)
        {
            var validator = await _validator.ValidateAsync(input);
            if(!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return View(input);
            }

            var user = _mapper.Map<AppUser>(input);
            var userCreatedResult = await _userManager.CreateAsync(user,input.Password);
            if(!userCreatedResult.Succeeded)
            {
                ModelState.AddModelStateListErrors(userCreatedResult.Errors);
                return View(input);
            }

            return RedirectToAction("Login", "Authentication");
        }
    }
}
