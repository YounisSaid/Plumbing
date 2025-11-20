using EntityLayer.WebApp.ViewModels.HomePage;

namespace ServieceLayer.Serviecs.Abstract
{
    public interface IHomePageService
    {
        Task<List<HomePageListMV>> GetAllListAsync();
        Task<HomePageUpdateMV?> GetByIdAsync(int id);
        Task AddHomePageAsync(HomePageAddMV addMV);
        Task UpdateHomePageAsync(HomePageUpdateMV updateMV);
        Task DeleteHomePageAsync(int id);
    }
}