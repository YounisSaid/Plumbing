using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.Enumerates;
using EntityLayer.WebApplication.Entites;
using EntityLayer.WebApplication.ViewModels.About;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Exceptions.WebApplication;
using ServiceLayer.Helpers.Generic;
using ServiceLayer.Messages.WebApplication;
using ServiceLayer.Serviecs.WebApplication.Abstract;

namespace ServiceLayer.Serviecs.WebApplication.Concrete
{
    public class AboutService : IAboutService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<About> _aboutRepository;
        private readonly IImageHelper _imageHelper;
        private readonly IToastNotification _toasty;
        private const string Section = "About Section";
        public AboutService(IUnitOfWork unitOfWork, IMapper mapper, IImageHelper imageHelper, IToastNotification toasty)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _aboutRepository = _unitOfWork.GetRepository<About>();
            _imageHelper = imageHelper;
            _toasty = toasty;
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

            var image = await _imageHelper.UploadImageAsync(null, addVM.Photo, imageType.about);
            if (image.Error != null)
            {
                _toasty.AddErrorToastMessage(image.Error, new ToastrOptions { Title = NotificationMessagesWebApplication.FailedTitle });
                return;
            }
            addVM.FileName = image.FileName!;
            addVM.FileType = image.FileType!;
            var about = _mapper.Map<About>(addVM);
            await _aboutRepository.AddAsync(about);
            await _unitOfWork.CommitAsync();
            _toasty.AddSuccessToastMessage(NotificationMessagesWebApplication.AddMessage(Section), new ToastrOptions { Title = NotificationMessagesWebApplication.SuccessedTitle });
        }

        public async Task UpdateAboutAsync(AboutUpdateVM updateVM)
        {
            var oldAbout = await _aboutRepository.Where(x => x.Id == updateVM.Id).AsNoTracking().FirstAsync();
            if (updateVM.Photo != null)
            {
                var image = await _imageHelper.UploadImageAsync(null, updateVM.Photo, imageType.about);
                if (image.Error != null)
                {
                    _toasty.AddErrorToastMessage(image.Error, new ToastrOptions { Title = NotificationMessagesWebApplication.FailedTitle });
                    return;
                }
                updateVM.FileName = image.FileName!;
                updateVM.FileType = image.FileType!;
            }
            else
            {
                updateVM.FileName = oldAbout.FileName;
                updateVM.FileType = oldAbout.FileType;
            }

            var about = _mapper.Map<About>(updateVM);
            _aboutRepository.Update(about);
            bool result = await _unitOfWork.CommitAsync();
            if (!result)
            {
                _imageHelper.DeleteImage(updateVM.FileName);
                throw new ClientSideException(ExceptionMessages.ConcurrencyException);
            }

            if (updateVM.Photo != null)
            {
                _imageHelper.DeleteImage(oldAbout!.FileName);
            }
            _toasty.AddWarningToastMessage(NotificationMessagesWebApplication.UpdateMessage(Section), new ToastrOptions { Title = NotificationMessagesWebApplication.WarningTitle });

        }

        public async Task DeleteAboutAsync(int id)
        {
            var about = await _aboutRepository.GetByIdAsync(id);
            _aboutRepository.Delete(about!);
            await _unitOfWork.CommitAsync();
            _imageHelper.DeleteImage(about!.FileName);
            _toasty.AddWarningToastMessage(NotificationMessagesWebApplication.DeleteMessage(Section), new ToastrOptions { Title = NotificationMessagesWebApplication.WarningTitle });

        }
        //UI for Services
        public async Task<List<AboutListMVForUi>> GetAllListForUiAsync()
        {
            var aboutsUi = await _aboutRepository.GetAll().ProjectTo<AboutListMVForUi>(_mapper.ConfigurationProvider).ToListAsync();
            return aboutsUi;
        }
    }
}
