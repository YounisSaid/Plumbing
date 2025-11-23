using EntityLayer.WebApp.ViewModels.Portfolio;
using Microsoft.AspNetCore.Mvc;
using ServieceLayer.Serviecs.Abstract;
using System.Threading.Tasks;

namespace Plumbing.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PortfolioController : Controller
    {
        private readonly IPortfolioService _portfolioService;

        public PortfolioController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        public async Task<IActionResult> GetPortfolioList()
        {
            var portfolioList = await _portfolioService.GetAllListAsync();
            return View(portfolioList);
        }

        [HttpGet]
        public async Task<IActionResult> AddPortfolio()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPortfolio(PortfolioAddMV model)
        {
            await _portfolioService.AddPortfolioAsync(model);
            return RedirectToAction(nameof(GetPortfolioList), "Portfolio", new { Area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePortfolio(int Id)
        {
            var portfolio = await _portfolioService.GetByIdAsync(Id);
            return View(portfolio);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePortfolio(PortfolioUpdateMV model)
        {
            await _portfolioService.UpdatePortfolioAsync(model);
            return RedirectToAction(nameof(GetPortfolioList), "Portfolio", new { Area = "Admin" });
        }

        public async Task<IActionResult> DeletePortfolio(int Id)
        {
            await _portfolioService.DeletePortfolioAsync(Id);
            return RedirectToAction(nameof(GetPortfolioList), "Portfolio", new { Area = "Admin" });
        }
    }
}