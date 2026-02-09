using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApplication.Entites;
using EntityLayer.WebApplication.ViewModels.HomePage;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Exceptions.WebApplication;
using ServiceLayer.Messages.WebApplication;
using ServiceLayer.Serviecs.WebApplication.Abstract;

namespace ServiceLayer.Serviecs.WebApplication.Concrete
{
    public class HomePageService : IHomePageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<HomePage> _homePageRepository;
        private readonly IToastNotification _toasty;
        private const string Section = "HomePage Section";
        public HomePageService(IUnitOfWork unitOfWork, IMapper mapper, IToastNotification toasty)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _homePageRepository = _unitOfWork.GetRepository<HomePage>();
            _toasty = toasty;
        }

        public async Task<List<HomePageListMV>> GetAllListAsync()
        {
            var homePages = await _homePageRepository.GetAll().ProjectTo<HomePageListMV>(_mapper.ConfigurationProvider).ToListAsync();
            return homePages;
        }

        public async Task<HomePageUpdateMV?> GetByIdAsync(int id)
        {
            var homePage = await _homePageRepository.Where(x => x.Id == id)
                .ProjectTo<HomePageUpdateMV>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return homePage;
        }

        public async Task AddHomePageAsync(HomePageAddMV addMV)
        {
            var homePage = _mapper.Map<HomePage>(addMV);
            await _homePageRepository.AddAsync(homePage);
            await _unitOfWork.CommitAsync();
            _toasty.AddSuccessToastMessage(NotificationMessagesWebApplication.AddMessage(Section), new ToastrOptions { Title = NotificationMessagesWebApplication.SuccessedTitle });

        }

        public async Task UpdateHomePageAsync(HomePageUpdateMV updateMV)
        {
            var homePage = _mapper.Map<HomePage>(updateMV);
            _homePageRepository.Update(homePage);
            bool result = await _unitOfWork.CommitAsync();
            if (!result)
            {

                throw new ClientSideException(ExceptionMessages.ConcurrencyException);
            }
            _toasty.AddWarningToastMessage(NotificationMessagesWebApplication.UpdateMessage(Section), new ToastrOptions { Title = NotificationMessagesWebApplication.WarningTitle });


        }

        public async Task DeleteHomePageAsync(int id)
        {
            var homePage = await _homePageRepository.GetByIdAsync(id);
            _homePageRepository.Delete(homePage!);
            await _unitOfWork.CommitAsync();
            _toasty.AddWarningToastMessage(NotificationMessagesWebApplication.DeleteMessage(Section), new ToastrOptions { Title = NotificationMessagesWebApplication.WarningTitle });
        }
    }
}