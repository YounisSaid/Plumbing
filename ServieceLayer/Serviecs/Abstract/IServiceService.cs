using EntityLayer.WebApp.ViewModels.Service;

namespace ServieceLayer.Serviecs.Abstract
{
    public interface IServiceService
    {
        Task<List<ServiceListMV>> GetAllListAsync();
        Task<ServiceUpdateMV?> GetByIdAsync(int id);
        Task AddServiceAsync(ServiceAddMV addMV);
        Task UpdateServiceAsync(ServiceUpdateMV updateMV);
        Task DeleteServiceAsync(int id);
    }
}