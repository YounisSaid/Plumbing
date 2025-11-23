using EntityLayer.WebApp.ViewModels.HomePage;
using Microsoft.AspNetCore.Mvc;
using ServieceLayer.Serviecs.Abstract;
using System.Threading.Tasks;

namespace Plumbing.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomePageController : Controller
    {
        private readonly IHomePageService _homePageService;

        public HomePageController(IHomePageService homePageService)
        {
            _homePageService = homePageService;
        }

        public async Task<IActionResult> GetHomePageList()
        {
            var homePageList = await _homePageService.GetAllListAsync();
            return View(homePageList);
        }

        [HttpGet]
        public async Task<IActionResult> AddHomePage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddHomePage(HomePageAddMV model)
        {
            await _homePageService.AddHomePageAsync(model);
            return RedirectToAction(nameof(GetHomePageList), "HomePage", new { Area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateHomePage(int Id)
        {
            var homePage = await _homePageService.GetByIdAsync(Id);
            return View(homePage);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateHomePage(HomePageUpdateMV model)
        {
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