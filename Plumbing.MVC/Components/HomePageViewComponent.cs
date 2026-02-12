using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Serviecs.WebApplication.Abstract;

namespace Plumbing.MVC.Components
{
    public class HomePageViewComponent : ViewComponent
    {
        private readonly IHomePageService _homePageService;

        public HomePageViewComponent(IHomePageService homePageService)
        {
            _homePageService = homePageService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var uiHomePageList = await _homePageService.GetAllListForUiAsync();
            return View(uiHomePageList);
        }

    }
}
