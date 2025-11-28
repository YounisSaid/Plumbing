using EntityLayer.WebApp.ViewModels.Testimonial;
using Microsoft.AspNetCore.Mvc;
using ServieceLayer.Serviecs.WebApplication.Abstract;
using System.Threading.Tasks;

namespace Plumbing.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TestimonialController : Controller
    {
        private readonly ITestimonialService _testimonialService;

        public TestimonialController(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }

        public async Task<IActionResult> GetTestimonialList()
        {
            var testimonialList = await _testimonialService.GetAllListAsync();
            return View(testimonialList);
        }

        [HttpGet]
        public async Task<IActionResult> AddTestimonial()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTestimonial(TestimonialAddMV model)
        {
            await _testimonialService.AddTestimonialAsync(model);
            return RedirectToAction(nameof(GetTestimonialList), "Testimonial", new { Area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTestimonial(int Id)
        {
            var testimonial = await _testimonialService.GetByIdAsync(Id);
            return View(testimonial);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTestimonial(TestimonialUpdateMV model)
        {
            await _testimonialService.UpdateTestimonialAsync(model);
            return RedirectToAction(nameof(GetTestimonialList), "Testimonial", new { Area = "Admin" });
        }

        public async Task<IActionResult> DeleteTestimonial(int Id)
        {
            await _testimonialService.DeleteTestimonialAsync(Id);
            return RedirectToAction(nameof(GetTestimonialList), "Testimonial", new { Area = "Admin" });
        }
    }
}