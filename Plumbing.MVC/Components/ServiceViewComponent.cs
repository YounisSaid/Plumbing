using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Serviecs.WebApplication.Abstract;

namespace Plumbing.MVC.Components
{
    public class ServiceViewComponent : ViewComponent
    {
        private readonly IServiceService _serviceService;

        public ServiceViewComponent(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var uiServiceList = await _serviceService.GetAllListForUiAsync();
            return View(uiServiceList);
        }
    }
}
