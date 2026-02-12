using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Serviecs.WebApplication.Abstract;

namespace Plumbing.MVC.Components
{
    public class PortfolioViewComponent : ViewComponent
    {
        private readonly IPortfolioService _portfolioService;

        public PortfolioViewComponent(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var uiPortfolioList = await _portfolioService.GetAllListForUiAsync();
            return View(uiPortfolioList);
        }
    }
}
