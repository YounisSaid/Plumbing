using EntityLayer.WebApplication.ViewModels.Portfolio;

namespace ServiceLayer.Serviecs.WebApplication.Abstract
{
    public interface IPortfolioService
    {
        Task<List<PortfolioListMV>> GetAllListAsync();
        Task<PortfolioUpdateMV?> GetByIdAsync(int id);
        Task AddPortfolioAsync(PortfolioAddMV addMV);
        Task UpdatePortfolioAsync(PortfolioUpdateMV updateMV);
        Task DeletePortfolioAsync(int id);
        Task<List<PortfolioListMVForUi>> GetAllListForUiAsync();
    }
}