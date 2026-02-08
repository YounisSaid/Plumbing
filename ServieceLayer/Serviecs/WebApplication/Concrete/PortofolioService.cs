using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.Enumerates;
using EntityLayer.WebApplication.Entites;
using EntityLayer.WebApplication.ViewModels.Portfolio;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Helpers.Generic;
using ServiceLayer.Messages.WebApplication;
using ServiceLayer.Serviecs.WebApplication.Abstract;

namespace ServiceLayer.Serviecs.WebApplication.Concrete
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Portfolio> _portfolioRepository;
        public readonly IImageHelper _imageHelper;
        private readonly IToastNotification _toasty;
        private const string Section = "Portfolio Section";


        public PortfolioService(IUnitOfWork unitOfWork, IMapper mapper, IImageHelper imageHelper, IToastNotification toasty)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _portfolioRepository = _unitOfWork.GetRepository<Portfolio>();
            _imageHelper = imageHelper;
            _toasty = toasty;
        }

        public async Task<List<PortfolioListMV>> GetAllListAsync()
        {
            var portfolios = await _portfolioRepository.GetAll().ProjectTo<PortfolioListMV>(_mapper.ConfigurationProvider).ToListAsync();
            return portfolios;
        }

        public async Task<PortfolioUpdateMV?> GetByIdAsync(int id)
        {
            var portfolio = await _portfolioRepository.Where(x => x.Id == id)
                .ProjectTo<PortfolioUpdateMV>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return portfolio;
        }

        public async Task AddPortfolioAsync(PortfolioAddMV addMV)
        {
            var image = await _imageHelper.UploadImageAsync(null, addMV.Photo, imageType.portifolio);
            if (image.Error != null)
            {
                _toasty.AddErrorToastMessage(image.Error, new ToastrOptions { Title = NotificationMessagesWebApplication.FailedTitle });
                return;
            }
            addMV.FileName = image.FileName!;
            addMV.FileType = image.FileType!;
            var portfolio = _mapper.Map<Portfolio>(addMV);
            await _portfolioRepository.AddAsync(portfolio);
            await _unitOfWork.CommitAsync();
            _toasty.AddSuccessToastMessage(NotificationMessagesWebApplication.AddMessage(Section), new ToastrOptions { Title = NotificationMessagesWebApplication.SuccessedTitle });

        }

        public async Task UpdatePortfolioAsync(PortfolioUpdateMV updateMV)
        {
            var oldpPortfolio = await _portfolioRepository.Where(x => x.Id == updateMV.Id).AsNoTracking().FirstAsync();
            if (updateMV.Photo != null)
            {
                var image = await _imageHelper.UploadImageAsync(null, updateMV.Photo, imageType.about);
                if (image.Error != null)
                {
                    _toasty.AddErrorToastMessage(image.Error, new ToastrOptions { Title = NotificationMessagesWebApplication.FailedTitle });
                    return;
                }
                updateMV.FileName = image.FileName!;
                updateMV.FileType = image.FileType!;
            }
            else
            {
                updateMV.FileName = oldpPortfolio.FileName;
                updateMV.FileType = oldpPortfolio.FileType;
            }
            var portfolio = _mapper.Map<Portfolio>(updateMV);
            _portfolioRepository.Update(portfolio);
            await _unitOfWork.CommitAsync();

            if (updateMV.Photo != null)
            {
                _imageHelper.DeleteImage(oldpPortfolio!.FileName);
            }
            _toasty.AddWarningToastMessage(NotificationMessagesWebApplication.UpdateMessage(Section), new ToastrOptions { Title = NotificationMessagesWebApplication.WarningTitle });
        }

        public async Task DeletePortfolioAsync(int id)
        {
            var portfolio = await _portfolioRepository.GetByIdAsync(id);
            _portfolioRepository.Delete(portfolio!);
            await _unitOfWork.CommitAsync();
            _imageHelper.DeleteImage(portfolio!.FileName);
            _toasty.AddWarningToastMessage(NotificationMessagesWebApplication.DeleteMessage(Section), new ToastrOptions { Title = NotificationMessagesWebApplication.WarningTitle });
        }
    }
}