using EntityLayer.WebApplication.ViewModels.Contact;

namespace ServiceLayer.Serviecs.WebApplication.Abstract
{
    public interface IContactService
    {
        Task<List<ContactListMV>> GetAllListAsync();
        Task<ContactUpdateMV?> GetByIdAsync(int id);
        Task AddContactAsync(ContactAddMV addMV);
        Task UpdateContactAsync(ContactUpdateMV updateMV);
        Task DeleteContactAsync(int id);
        Task<List<ContactListMVForUi>> GetAllListForUiAsync();
    }
}