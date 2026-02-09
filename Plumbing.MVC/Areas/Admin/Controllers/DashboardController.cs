using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Plumbing.MVC.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        [Authorize(Roles = "SuperAdmin")]
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
