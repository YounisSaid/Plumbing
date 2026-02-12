using Microsoft.AspNetCore.Mvc;

namespace Plumbing.MVC.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}
