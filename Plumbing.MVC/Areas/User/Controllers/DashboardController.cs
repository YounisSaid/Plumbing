using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Plumbing.MVC.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "SuperAdmin,Member")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
