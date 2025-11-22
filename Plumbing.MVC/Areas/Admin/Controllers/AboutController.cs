using EntityLayer.WebApp.ViewModels.About;
using Microsoft.AspNetCore.Mvc;
using ServieceLayer.Serviecs.Abstract;

namespace Plumbing.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;
        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }
      

        public async Task<IActionResult> GetAboutList()
        {
            var aboutList = await _aboutService.GetAllListAsync();
            return View(aboutList);
        }

        [HttpGet]
        public async Task<IActionResult> AddAbout()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAbout(AboutAddVM model)
        {
            await _aboutService.AddAboutAsync(model);
            return RedirectToAction(nameof(GetAboutList), "About", new { Area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAbout(int Id)
        {
            var about = await _aboutService.GetByIdAsync(Id);

            return View(about);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAbout(AboutUpdateVM model)
        {
            await _aboutService.UpdateAboutAsync(model);
            return RedirectToAction(nameof(GetAboutList), "About", new { Area = "Admin" });
        }


        public async Task<IActionResult> DeleteAbout(int Id)
        {
            await _aboutService.DeleteAboutAsync(Id);
            return RedirectToAction(nameof(GetAboutList), "About", new { Area = "Admin" });
        }
    }
}
