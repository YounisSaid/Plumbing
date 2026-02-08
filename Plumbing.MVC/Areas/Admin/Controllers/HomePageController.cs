using EntityLayer.WebApplication.Entites;
using EntityLayer.WebApplication.ViewModels.HomePage;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Filters.WebApplication;
using ServiceLayer.Serviecs.WebApplication.Abstract;

namespace Plumbing.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomePageController : Controller
    {
        private readonly IHomePageService _homePageService;
        private readonly IValidator<HomePageAddMV> _homePageAddValidator;
        private readonly IValidator<HomePageUpdateMV> _homePageUpdateValidator;
        public HomePageController(IHomePageService homePageService, IValidator<HomePageAddMV> homePageAddValidator, IValidator<HomePageUpdateMV> homePageUpdateValidator)
        {
            _homePageService = homePageService;
            _homePageAddValidator = homePageAddValidator;
            _homePageUpdateValidator = homePageUpdateValidator;
        }

        public async Task<IActionResult> GetHomePageList()
        {
            var homePageList = await _homePageService.GetAllListAsync();
            return View(homePageList);
        }

        [HttpGet]
        [ServiceFilter(typeof(AddGenericPreventionFilter<HomePage>))]
        public async Task<IActionResult> AddHomePage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddHomePage(HomePageAddMV model)
        {
            var ValidationResult = await _homePageAddValidator.ValidateAsync(model);
            if (ValidationResult.IsValid)
            {
                await _homePageService.AddHomePageAsync(model);
                return RedirectToAction(nameof(GetHomePageList), "HomePage", new { Area = "Admin" });
            }

            ValidationResult.AddToModelState(ModelState);
            return View(model);
        }

        [HttpGet]
        [ServiceFilter(typeof(GenericNotFoundFilter<HomePage>))]

        public async Task<IActionResult> UpdateHomePage(int Id)
        {
            var homePage = await _homePageService.GetByIdAsync(Id);
            return View(homePage);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateHomePage(HomePageUpdateMV model)
        {
            var ValidationResult = await _homePageUpdateValidator.ValidateAsync(model);
            if (!ValidationResult.IsValid)
            {
                ValidationResult.AddToModelState(ModelState);
                return View(model);
            }
            await _homePageService.UpdateHomePageAsync(model);
            return RedirectToAction(nameof(GetHomePageList), "HomePage", new { Area = "Admin" });
        }

        public async Task<IActionResult> DeleteHomePage(int Id)
        {
            await _homePageService.DeleteHomePageAsync(Id);
            return RedirectToAction(nameof(GetHomePageList), "HomePage", new { Area = "Admin" });
        }
    }
}