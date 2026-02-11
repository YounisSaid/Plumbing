using EntityLayer.WebApplication.ViewModels.UserList;

namespace ServiceLayer.Serviecs.Identity.Abstract
{
    public interface IAuthenticationUserListService
    {
        public Task<List<UserVM>> GetUserListAsync();
        public Task ExtendClaimAsync(string username);
    }
}
