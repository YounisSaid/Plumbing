using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Serviecs.WebApplication.Abstract;

namespace Plumbing.MVC.Areas.Admin.Controllers
{

    [Authorize(Policy = "AdminObserver")]
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CategoriesCount = await _dashboardService.GetCategoriesCountAsync();
            ViewBag.PortofliosCount = await _dashboardService.GetPortofliosCountAsync();
            ViewBag.ServicesCount = await _dashboardService.GetServicesCountAsync();
            ViewBag.TeamsCount = await _dashboardService.GetTeamsCountAsync();
            ViewBag.TestimonialCount = await _dashboardService.GetTestimonialCountAsync();
            ViewBag.UsersCount = await _dashboardService.GetUsersCountAsync();
            return View();
        }
    }
}
