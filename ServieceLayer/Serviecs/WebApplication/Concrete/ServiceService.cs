using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApplication.Entites;
using EntityLayer.WebApplication.ViewModels.Service;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Exceptions.WebApplication;
using ServiceLayer.Messages.WebApplication;
using ServiceLayer.Serviecs.WebApplication.Abstract;

namespace ServiceLayer.Serviecs.WebApplication.Concrete
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Service> _serviceRepository;
        private readonly IToastNotification _toasty;
        private const string Section = "Service Section";

        public ServiceService(IUnitOfWork unitOfWork, IMapper mapper, IToastNotification toasty)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _serviceRepository = _unitOfWork.GetRepository<Service>();
            _toasty = toasty;
        }

        public async Task<List<ServiceListMV>> GetAllListAsync()
        {
            var services = await _serviceRepository.GetAll().ProjectTo<ServiceListMV>(_mapper.ConfigurationProvider).ToListAsync();
            return services;
        }

        public async Task<ServiceUpdateMV?> GetByIdAsync(int id)
        {
            var service = await _serviceRepository.Where(x => x.Id == id)
                .ProjectTo<ServiceUpdateMV>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return service;
        }

        public async Task AddServiceAsync(ServiceAddMV addMV)
        {
            var service = _mapper.Map<Service>(addMV);
            await _serviceRepository.AddAsync(service);
            await _unitOfWork.CommitAsync();
            _toasty.AddSuccessToastMessage(NotificationMessagesWebApplication.AddMessage(Section), new ToastrOptions { Title = NotificationMessagesWebApplication.SuccessedTitle });

        }

        public async Task UpdateServiceAsync(ServiceUpdateMV updateMV)
        {
            var service = _mapper.Map<Service>(updateMV);
            _serviceRepository.Update(service);
            bool result = await _unitOfWork.CommitAsync();
            if (!result)
            {

                throw new ClientSideException(ExceptionMessages.ConcurrencyException);
            }
            _toasty.AddWarningToastMessage(NotificationMessagesWebApplication.UpdateMessage(Section), new ToastrOptions { Title = NotificationMessagesWebApplication.WarningTitle });
        }

        public async Task DeleteServiceAsync(int id)
        {
            var service = await _serviceRepository.GetByIdAsync(id);
            _serviceRepository.Delete(service!);
            await _unitOfWork.CommitAsync();
            _toasty.AddWarningToastMessage(NotificationMessagesWebApplication.DeleteMessage(Section), new ToastrOptions { Title = NotificationMessagesWebApplication.WarningTitle });
        }
    }
}