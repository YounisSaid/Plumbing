using EntityLayer.WebApplication.ViewModels.SocialMedia;

namespace ServiceLayer.Serviecs.WebApplication.Abstract
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