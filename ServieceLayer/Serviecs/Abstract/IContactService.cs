using EntityLayer.WebApp.ViewModels.Contact;

namespace ServieceLayer.Serviecs.Abstract
{
    public interface IContactService
    {
        Task<List<ContactListMV>> GetAllListAsync();
        Task<ContactUpdateMV?> GetByIdAsync(int id);
        Task AddContactAsync(ContactAddMV addMV);
        Task UpdateContactAsync(ContactUpdateMV updateMV);
        Task DeleteContactAsync(int id);
    }
}