using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApplication.Entites;
using EntityLayer.WebApplication.ViewModels.Contact;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Exceptions.WebApplication;
using ServiceLayer.Messages.WebApplication;
using ServiceLayer.Serviecs.WebApplication.Abstract;

namespace ServiceLayer.Serviecs.WebApplication.Concrete
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Contact> _contactRepository;
        private readonly IToastNotification _toasty;
        private const string Section = "Contact Section";
        public ContactService(IUnitOfWork unitOfWork, IMapper mapper, IToastNotification toasty)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contactRepository = _unitOfWork.GetRepository<Contact>();
            _toasty = toasty;
        }

        public async Task<List<ContactListMV>> GetAllListAsync()
        {
            var contacts = await _contactRepository.GetAll().ProjectTo<ContactListMV>(_mapper.ConfigurationProvider).ToListAsync();
            return contacts;
        }

        public async Task<ContactUpdateMV?> GetByIdAsync(int id)
        {
            var contact = await _contactRepository.Where(x => x.Id == id)
                .ProjectTo<ContactUpdateMV>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return contact;
        }

        public async Task AddContactAsync(ContactAddMV addMV)
        {
            var contact = _mapper.Map<Contact>(addMV);
            await _contactRepository.AddAsync(contact);
            await _unitOfWork.CommitAsync();
            _toasty.AddSuccessToastMessage(NotificationMessagesWebApplication.AddMessage(Section), new ToastrOptions { Title = NotificationMessagesWebApplication.SuccessedTitle });

        }

        public async Task UpdateContactAsync(ContactUpdateMV updateMV)
        {
            var contact = _mapper.Map<Contact>(updateMV);
            _contactRepository.Update(contact);
            bool result = await _unitOfWork.CommitAsync();
            if (!result)
            {

                throw new ClientSideException(ExceptionMessages.ConcurrencyException);
            }
            _toasty.AddWarningToastMessage(NotificationMessagesWebApplication.UpdateMessage(Section), new ToastrOptions { Title = NotificationMessagesWebApplication.WarningTitle });

        }

        public async Task DeleteContactAsync(int id)
        {
            var contact = await _contactRepository.GetByIdAsync(id);
            _contactRepository.Delete(contact!);
            await _unitOfWork.CommitAsync();
            _toasty.AddWarningToastMessage(NotificationMessagesWebApplication.DeleteMessage(Section), new ToastrOptions { Title = NotificationMessagesWebApplication.WarningTitle });
        }
    }
}