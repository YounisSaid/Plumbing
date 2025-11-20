using EntityLayer.WebApp.ViewModels.SocialMedia;

namespace ServieceLayer.Serviecs.Abstract
{
    public interface ISocialMediaService
    {
        Task<List<SocialMediaListMV>> GetAllListAsync();
        Task<SocialMediaUpdateMV?> GetByIdAsync(int id);
        Task AddSocialMediaAsync(SocialMediaAddMV addMV);
        Task UpdateSocialMediaAsync(SocialMediaUpdateMV updateMV);
        Task DeleteSocialMediaAsync(int id);
    }
}