using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApplication.Entites;
using EntityLayer.WebApplication.ViewModels.Contact;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Serviecs.WebApplication.Abstract;

namespace ServiceLayer.Serviecs.WebApplication.Concrete
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Contact> _contactRepository;

        public ContactService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contactRepository = _unitOfWork.GetRepository<Contact>();
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
        }

        public async Task UpdateContactAsync(ContactUpdateMV updateMV)
        {
            var contact = _mapper.Map<Contact>(updateMV);
            _contactRepository.Update(contact);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteContactAsync(int id)
        {
            var contact = await _contactRepository.GetByIdAsync(id);
            _contactRepository.Delete(contact!);
            await _unitOfWork.CommitAsync();
        }
    }
}