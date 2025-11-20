using EntityLayer.WebApp.ViewModels.About;

namespace ServieceLayer.Serviecs.Abstract
{
    public interface IAboutService
    {
        Task<List<AboutListMV>> GetAllListAsync();
        Task<AboutUpdateVM?> GetByIdAsync(int id);
        Task AddAboutAsync(AboutAddVM addVM);
        Task UpdateAboutAsync(AboutUpdateVM updateVM);
        Task DeleteAboutAsync(int id);
    }
}
