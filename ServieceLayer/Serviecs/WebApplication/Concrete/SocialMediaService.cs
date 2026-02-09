using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApplication.Entites;
using EntityLayer.WebApplication.ViewModels.SocialMedia;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Exceptions.WebApplication;
using ServiceLayer.Messages.WebApplication;
using ServiceLayer.Serviecs.WebApplication.Abstract;

namespace ServiceLayer.Serviecs.WebApplication.Concrete
{
    public class SocialMediaService : ISocialMediaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<SocialMedia> _socialMediaRepository;
        private readonly IToastNotification _toasty;
        private const string Section = "SocialMedia Section";


        public SocialMediaService(IUnitOfWork unitOfWork, IMapper mapper, IToastNotification toasty)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _socialMediaRepository = _unitOfWork.GetRepository<SocialMedia>();
            _toasty = toasty;
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
            _toasty.AddSuccessToastMessage(NotificationMessagesWebApplication.AddMessage(Section), new ToastrOptions { Title = NotificationMessagesWebApplication.SuccessedTitle });

        }

        public async Task UpdateSocialMediaAsync(SocialMediaUpdateMV updateMV)
        {
            var socialMedia = _mapper.Map<SocialMedia>(updateMV);
            _socialMediaRepository.Update(socialMedia);
            bool result = await _unitOfWork.CommitAsync();
            if (!result)
            {

                throw new ClientSideException(ExceptionMessages.ConcurrencyException);
            }
            _toasty.AddWarningToastMessage(NotificationMessagesWebApplication.UpdateMessage(Section), new ToastrOptions { Title = NotificationMessagesWebApplication.WarningTitle });


        }

        public async Task DeleteSocialMediaAsync(int id)
        {
            var socialMedia = await _socialMediaRepository.GetByIdAsync(id);
            _socialMediaRepository.Delete(socialMedia!);
            await _unitOfWork.CommitAsync();
            _toasty.AddWarningToastMessage(NotificationMessagesWebApplication.DeleteMessage(Section), new ToastrOptions { Title = NotificationMessagesWebApplication.WarningTitle });
        }
    }
}