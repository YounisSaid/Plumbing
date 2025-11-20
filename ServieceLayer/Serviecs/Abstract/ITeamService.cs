using EntityLayer.WebApp.ViewModels.Team;

namespace ServieceLayer.Serviecs.Abstract
{
    public interface ITeamService
    {
        Task<List<TeamListMV>> GetAllListAsync();
        Task<TeamUpdateMV?> GetByIdAsync(int id);
        Task AddTeamAsync(TeamAddMV addMV);
        Task UpdateTeamAsync(TeamUpdateMV updateMV);
        Task DeleteTeamAsync(int id);
    }
}