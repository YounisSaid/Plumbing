using EntityLayer.WebApplication.ViewModels.Service;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Serviecs.WebApplication.Abstract;
using System.Threading.Tasks;

namespace Plumbing.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
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
            await _serviceService.AddServiceAsync(model);
            return RedirectToAction(nameof(GetServiceList), "Service", new { Area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateService(int Id)
        {
            var service = await _serviceService.GetByIdAsync(Id);
            return View(service);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateService(ServiceUpdateMV model)
        {
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