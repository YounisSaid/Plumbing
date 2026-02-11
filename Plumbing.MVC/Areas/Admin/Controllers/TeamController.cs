using EntityLayer.WebApplication.Entites;
using EntityLayer.WebApplication.ViewModels.Team;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Filters.WebApplication;
using ServiceLayer.Serviecs.WebApplication.Abstract;

namespace Plumbing.MVC.Areas.Admin.Controllers
{
    [Authorize(Policy = "AdminObserver")]
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;
        private readonly IValidator<TeamAddMV> _teamAddValidator;
        private readonly IValidator<TeamUpdateMV> _teamUpdateValidator;

        public TeamController(ITeamService teamService, IValidator<TeamAddMV> teamAddValidator, IValidator<TeamUpdateMV> teamUpdateValidator)
        {
            _teamService = teamService;
            _teamAddValidator = teamAddValidator;
            _teamUpdateValidator = teamUpdateValidator;
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
            var ValidationResult = await _teamAddValidator.ValidateAsync(model);
            if (!ValidationResult.IsValid)
            {
                ValidationResult.AddToModelState(ModelState);
                return View(model);
            }
            await _teamService.AddTeamAsync(model);
            return RedirectToAction(nameof(GetTeamList), "Team", new { Area = "Admin" });
        }

        [HttpGet]
        [ServiceFilter(typeof(GenericNotFoundFilter<Team>))]

        public async Task<IActionResult> UpdateTeam(int Id)
        {
            var team = await _teamService.GetByIdAsync(Id);
            return View(team);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTeam(TeamUpdateMV model)
        {
            var ValidationResult = await _teamUpdateValidator.ValidateAsync(model);
            if (!ValidationResult.IsValid)
            {
                ValidationResult.AddToModelState(ModelState);
                return View(model);
            }
            await _teamService.UpdateTeamAsync(model);
            return RedirectToAction(nameof(GetTeamList), "Team", new { Area = "Admin" });
        }
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteTeam(int Id)
        {
            await _teamService.DeleteTeamAsync(Id);
            return RedirectToAction(nameof(GetTeamList), "Team", new { Area = "Admin" });
        }
    }
}