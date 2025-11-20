using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApp.Entites;
using EntityLayer.WebApp.ViewModels.SocialMedia;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServieceLayer.Serviecs.Abstract;

namespace ServieceLayer.Serviecs.Concrete
{
    public class SocialMediaService : ISocialMediaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<SocialMedia> _socialMediaRepository;

        public SocialMediaService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _socialMediaRepository = _unitOfWork.GetRepository<SocialMedia>();
        }

        public async Task<List<SocialMediaListMV>> GetAllListAsync()
        {
            var socialMedias = await _socialMediaRepository.GetAll().ProjectTo<SocialMediaListMV>(_mapper.ConfigurationProvider).ToListAsync();
            return socialMedias;
        }

        public async Task<SocialMediaUpdateMV?> GetByIdAsync(int id)
        {
            var socialMedia = await _socialMediaRepository.Where(x => x.Id == id)
                .ProjectTo<SocialMediaUpdateMV>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return socialMedia;
        }

        public async Task AddSocialMediaAsync(SocialMediaAddMV addMV)
        {
            var socialMedia = _mapper.Map<SocialMedia>(addMV);
            await _socialMediaRepository.AddAsync(socialMedia);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateSocialMediaAsync(SocialMediaUpdateMV updateMV)
        {
            var socialMedia = _mapper.Map<SocialMedia>(updateMV);
            _socialMediaRepository.Update(socialMedia);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteSocialMediaAsync(int id)
        {
            var socialMedia = await _socialMediaRepository.GetByIdAsync(id);
            _socialMediaRepository.Delete(socialMedia);
            await _unitOfWork.CommitAsync();
        }
    }
}