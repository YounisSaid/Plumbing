using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApp.Entites;
using EntityLayer.WebApp.ViewModels.About;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServieceLayer.Serviecs.Abstract;

namespace ServieceLayer.Serviecs.Concrete
{
    public class AboutService : IAboutService
    {
        private readonly IUnitOfWork _unitOfWork;  
        private readonly IMapper _mapper;
        private readonly IGenericRepository<About> _aboutRepository;
        public AboutService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _aboutRepository = _unitOfWork.GetRepository<About>();
        }

        public async Task<List<AboutListMV>> GetAllListAsync()
        {
            var abouts = await _aboutRepository.GetAll().ProjectTo<AboutListMV>(_mapper.ConfigurationProvider).ToListAsync();
            return abouts;
        }

        public async Task<AboutUpdateVM?> GetByIdAsync(int id)
        {
            var about = await _aboutRepository.Where(x => x.Id == id)
                .ProjectTo<AboutUpdateVM>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return about;
        }

        public async Task AddAboutAsync(AboutAddVM addVM)
        {
            var about = _mapper.Map<About>(addVM);
            await _aboutRepository.AddAsync(about);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAboutAsync(AboutUpdateVM updateVM)
        {
            var about = _mapper.Map<About>(updateVM);
            _aboutRepository.Update(about);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAboutAsync(int id)
        {
            var about = await _aboutRepository.GetByIdAsync(id);
             _aboutRepository.Delete(about);
             await _unitOfWork.CommitAsync();
            
        }
    }
}
