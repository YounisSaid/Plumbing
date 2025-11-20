using EntityLayer.WebApp.ViewModels.Portfolio;

namespace ServieceLayer.Serviecs.Abstract
{
    public interface IPortfolioService
    {
        Task<List<PortfolioListMV>> GetAllListAsync();
        Task<PortfolioUpdateMV?> GetByIdAsync(int id);
        Task AddPortfolioAsync(PortfolioAddMV addMV);
        Task UpdatePortfolioAsync(PortfolioUpdateMV updateMV);
        Task DeletePortfolioAsync(int id);
    }
}