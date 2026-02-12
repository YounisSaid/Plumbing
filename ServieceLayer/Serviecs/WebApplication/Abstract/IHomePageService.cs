using EntityLayer.WebApplication.ViewModels.HomePage;

namespace ServiceLayer.Serviecs.WebApplication.Abstract
{
    public interface IHomePageService
    {
        Task<List<HomePageListMV>> GetAllListAsync();
        Task<HomePageUpdateMV?> GetByIdAsync(int id);
        Task AddHomePageAsync(HomePageAddMV addMV);
        Task UpdateHomePageAsync(HomePageUpdateMV updateMV);
        Task DeleteHomePageAsync(int id);
        Task<List<HomePageListMVForUi>> GetAllListForUiAsync();
    }
}