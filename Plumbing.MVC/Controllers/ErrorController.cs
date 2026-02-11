using EntityLayer.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ServiceLayer.Exceptions.WebApplication;

namespace Plumbing.MVC.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult GeneralExceptions()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()!.Error;

            if (exception is ClientSideException)
            {
                return View(new ErrorVM(exception.Message, 401));
            }


            if (exception.InnerException is SqlException sqlException && sqlException.Number == 547)
            {
                return View(new ErrorVM("This Data Can't be Deleted Because it has Related Data To it,Please Remove it First!!", 401));
            }
            return View(new ErrorVM("Server Error!! Please Contact Your Admin", 500));
        }

        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}
