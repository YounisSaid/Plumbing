using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApp.Entites;
using EntityLayer.WebApp.ViewModels.Service;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServieceLayer.Serviecs.Abstract;

namespace ServieceLayer.Serviecs.Concrete
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Service> _serviceRepository;

        public ServiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _serviceRepository = _unitOfWork.GetRepository<Service>();
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
        }

        public async Task UpdateServiceAsync(ServiceUpdateMV updateMV)
        {
            var service = _mapper.Map<Service>(updateMV);
            _serviceRepository.Update(service);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteServiceAsync(int id)
        {
            var service = await _serviceRepository.GetByIdAsync(id);
            _serviceRepository.Delete(service);
            await _unitOfWork.CommitAsync();
        }
    }
}