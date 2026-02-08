using EntityLayer.WebApplication.Entites;
using EntityLayer.WebApplication.ViewModels.Testimonial;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Filters.WebApplication;
using ServiceLayer.Serviecs.WebApplication.Abstract;

namespace Plumbing.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TestimonialController : Controller
    {
        private readonly ITestimonialService _testimonialService;
        private readonly IValidator<TestimonialAddMV> _addValidator;
        private readonly IValidator<TestimonialUpdateMV> _updateValidator;

        public TestimonialController(ITestimonialService testimonialService, IValidator<TestimonialAddMV> addValidator, IValidator<TestimonialUpdateMV> updateValidator)
        {
            _testimonialService = testimonialService;
            _addValidator = addValidator;
            _updateValidator = updateValidator;
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
            var ValidationResult = await _addValidator.ValidateAsync(model);
            if (!ValidationResult.IsValid)
            {
                ValidationResult.AddToModelState(ModelState);
                return View(model);
            }
            await _testimonialService.AddTestimonialAsync(model);
            return RedirectToAction(nameof(GetTestimonialList), "Testimonial", new { Area = "Admin" });
        }

        [HttpGet]
        [ServiceFilter(typeof(GenericNotFoundFilter<Testimonial>))]

        public async Task<IActionResult> UpdateTestimonial(int Id)
        {
            var testimonial = await _testimonialService.GetByIdAsync(Id);
            return View(testimonial);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTestimonial(TestimonialUpdateMV model)
        {
            var ValidationResult = await _updateValidator.ValidateAsync(model);
            if (!ValidationResult.IsValid)
            {
                ValidationResult.AddToModelState(ModelState);
                return View(model);
            }
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