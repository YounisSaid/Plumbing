using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApp.Entites;
using EntityLayer.WebApp.ViewModels.Portfolio;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServieceLayer.Serviecs.Abstract;

namespace ServieceLayer.Serviecs.Concrete
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Portfolio> _portfolioRepository;

        public PortfolioService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _portfolioRepository = _unitOfWork.GetRepository<Portfolio>();
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
            var portfolio = _mapper.Map<Portfolio>(addMV);
            await _portfolioRepository.AddAsync(portfolio);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdatePortfolioAsync(PortfolioUpdateMV updateMV)
        {
            var portfolio = _mapper.Map<Portfolio>(updateMV);
            _portfolioRepository.Update(portfolio);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeletePortfolioAsync(int id)
        {
            var portfolio = await _portfolioRepository.GetByIdAsync(id);
            _portfolioRepository.Delete(portfolio);
            await _unitOfWork.CommitAsync();
        }
    }
}