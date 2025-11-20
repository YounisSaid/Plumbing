using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApp.Entites;
using EntityLayer.WebApp.ViewModels.HomePage;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServieceLayer.Serviecs.Abstract;

namespace ServieceLayer.Serviecs.Concrete
{
    public class HomePageService : IHomePageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<HomePage> _homePageRepository;

        public HomePageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _homePageRepository = _unitOfWork.GetRepository<HomePage>();
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
        }

        public async Task UpdateHomePageAsync(HomePageUpdateMV updateMV)
        {
            var homePage = _mapper.Map<HomePage>(updateMV);
            _homePageRepository.Update(homePage);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteHomePageAsync(int id)
        {
            var homePage = await _homePageRepository.GetByIdAsync(id);
            _homePageRepository.Delete(homePage);
            await _unitOfWork.CommitAsync();
        }
    }
}