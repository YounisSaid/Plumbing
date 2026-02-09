using EntityLayer.WebApplication.Entites;
using EntityLayer.WebApplication.ViewModels.Service;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Filters.WebApplication;
using ServiceLayer.Serviecs.WebApplication.Abstract;

namespace Plumbing.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;
        private readonly IValidator<ServiceAddMV> _serviceAddValidator;
        private readonly IValidator<ServiceUpdateMV> _serviceUpdateValidator;
        public ServiceController(IServiceService serviceService, IValidator<ServiceAddMV> serviceAddValidator, IValidator<ServiceUpdateMV> serviceUpdateValidator)
        {
            _serviceService = serviceService;
            _serviceAddValidator = serviceAddValidator;
            _serviceUpdateValidator = serviceUpdateValidator;
        }

        public async Task<IActionResult> GetServiceList()
        {
            var serviceList = await _serviceService.GetAllListAsync();
            return View(serviceList);
        }

        [HttpGet]
        public async Task<IActionResult> AddService()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddService(ServiceAddMV model)
        {
            var ValidationResult = await _serviceAddValidator.ValidateAsync(model);
            if (!ValidationResult.IsValid)
            {
                ValidationResult.AddToModelState(ModelState);
                return View(model);
            }
            await _serviceService.AddServiceAsync(model);
            return RedirectToAction(nameof(GetServiceList), "Service", new { Area = "Admin" });
        }

        [HttpGet]
        [ServiceFilter(typeof(GenericNotFoundFilter<Service>))]

        public async Task<IActionResult> UpdateService(int Id)
        {
            var service = await _serviceService.GetByIdAsync(Id);
            return View(service);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateService(ServiceUpdateMV model)
        {
            var ValidationResult = await _serviceUpdateValidator.ValidateAsync(model);
            if (!ValidationResult.IsValid)
            {
                ValidationResult.AddToModelState(ModelState);
                return View(model);
            }
            await _serviceService.UpdateServiceAsync(model);
            return RedirectToAction(nameof(GetServiceList), "Service", new { Area = "Admin" });
        }

        public async Task<IActionResult> DeleteService(int Id)
        {
            await _serviceService.DeleteServiceAsync(Id);
            return RedirectToAction(nameof(GetServiceList), "Service", new { Area = "Admin" });
        }
    }
}