using EntityLayer.WebApplication.ViewModels.Portfolio;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Serviecs.WebApplication.Abstract;

namespace Plumbing.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PortfolioController : Controller
    {
        private readonly IPortfolioService _portfolioService;
        private readonly IValidator<PortfolioAddMV> _portfolioAddValidator;
        private readonly IValidator<PortfolioUpdateMV> _portfolioUpdateValidator;

        public PortfolioController(IPortfolioService portfolioService, IValidator<PortfolioAddMV> portfolioAddValidator, IValidator<PortfolioUpdateMV> portfolioUpdateValidator)
        {
            _portfolioService = portfolioService;
            _portfolioAddValidator = portfolioAddValidator;
            _portfolioUpdateValidator = portfolioUpdateValidator;
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
            var ValidationResult = await _portfolioAddValidator.ValidateAsync(model);
            if (!ValidationResult.IsValid)
            {
                ValidationResult.AddToModelState(ModelState);
                return View(model);
            }
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
            var ValidationResult = await _portfolioUpdateValidator.ValidateAsync(model);
            if (!ValidationResult.IsValid)
            {
                ValidationResult.AddToModelState(ModelState);
                return View(model);
            }
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