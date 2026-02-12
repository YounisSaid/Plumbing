using EntityLayer.WebApplication.ViewModels.About;

namespace ServiceLayer.Serviecs.WebApplication.Abstract
{
    public interface IAboutService
    {
        Task<List<AboutListMV>> GetAllListAsync();
        Task<AboutUpdateVM?> GetByIdAsync(int id);
        Task AddAboutAsync(AboutAddVM addVM);
        Task UpdateAboutAsync(AboutUpdateVM updateVM);
        Task DeleteAboutAsync(int id);
        Task<List<AboutListMVForUi>> GetAllListForUiAsync();
    }
}
