using EntityLayer.WebApp.ViewModels.Team;
using Microsoft.AspNetCore.Mvc;
using ServieceLayer.Serviecs.Abstract;
using System.Threading.Tasks;

namespace Plumbing.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public async Task<IActionResult> GetTeamList()
        {
            var teamList = await _teamService.GetAllListAsync();
            return View(teamList);
        }

        [HttpGet]
        public async Task<IActionResult> AddTeam()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTeam(TeamAddMV model)
        {
            await _teamService.AddTeamAsync(model);
            return RedirectToAction(nameof(GetTeamList), "Team", new { Area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTeam(int Id)
        {
            var team = await _teamService.GetByIdAsync(Id);
            return View(team);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTeam(TeamUpdateMV model)
        {
            await _teamService.UpdateTeamAsync(model);
            return RedirectToAction(nameof(GetTeamList), "Team", new { Area = "Admin" });
        }

        public async Task<IActionResult> DeleteTeam(int Id)
        {
            await _teamService.DeleteTeamAsync(Id);
            return RedirectToAction(nameof(GetTeamList), "Team", new { Area = "Admin" });
        }
    }
}