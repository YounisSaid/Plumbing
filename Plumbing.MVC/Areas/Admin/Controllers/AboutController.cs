using EntityLayer.WebApplication.Entites;
using EntityLayer.WebApplication.ViewModels.About;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Filters.WebApplication;
using ServiceLayer.Serviecs.WebApplication.Abstract;

namespace Plumbing.MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;
        private readonly IValidator<AboutAddVM> _aboutAddValidator;
        private readonly IValidator<AboutUpdateVM> _aboutUpdateValidator;


        public AboutController(IAboutService aboutService, IValidator<AboutAddVM> aboutAddValidator, IValidator<AboutUpdateVM> aboutUpdateValidator)
        {
            _aboutService = aboutService;
            _aboutAddValidator = aboutAddValidator;
            _aboutUpdateValidator = aboutUpdateValidator;

        }


        public async Task<IActionResult> GetAboutList()
        {
            var aboutList = await _aboutService.GetAllListAsync();
            return View(aboutList);
        }

        [HttpGet]
        [ServiceFilter(typeof(AddGenericPreventionFilter<About>))]
        public async Task<IActionResult> AddAbout()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAbout(AboutAddVM model)
        {
            var ValidationResult = await _aboutAddValidator.ValidateAsync(model);
            if (ValidationResult.IsValid)
            {
                await _aboutService.AddAboutAsync(model);
                return RedirectToAction(nameof(GetAboutList), "About", new { Area = "Admin" });

            }
            ValidationResult.AddToModelState(ModelState);
            return View(model);
        }

        [HttpGet]
        [ServiceFilter(typeof(GenericNotFoundFilter<About>))]
        public async Task<IActionResult> UpdateAbout(int Id)
        {
            var about = await _aboutService.GetByIdAsync(Id);

            return View(about);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAbout(AboutUpdateVM model)
        {
            var ValidationResult = await _aboutUpdateValidator.ValidateAsync(model);
            if (ValidationResult.IsValid)
            {
                await _aboutService.UpdateAboutAsync(model);
                return RedirectToAction(nameof(GetAboutList), "About", new { Area = "Admin" });
            }
            ValidationResult.AddToModelState(ModelState);
            return View(model);
        }


        public async Task<IActionResult> DeleteAbout(int Id)
        {
            await _aboutService.DeleteAboutAsync(Id);
            return RedirectToAction(nameof(GetAboutList), "About", new { Area = "Admin" });
        }
    }
}
