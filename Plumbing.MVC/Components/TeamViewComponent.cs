using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Serviecs.WebApplication.Abstract;

namespace Plumbing.MVC.Components
{
    public class TeamViewComponent : ViewComponent
    {
        private readonly ITeamService _teamService;

        public TeamViewComponent(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var uiTeamList = await _teamService.GetAllListForUiAsync();
            return View(uiTeamList);
        }
    }
}
