using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApp.Entites;
using EntityLayer.WebApp.ViewModels.Team;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServieceLayer.Serviecs.Abstract;

namespace ServieceLayer.Serviecs.Concrete
{
    public class TeamService : ITeamService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Team> _teamRepository;

        public TeamService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _teamRepository = _unitOfWork.GetRepository<Team>();
        }

        public async Task<List<TeamListMV>> GetAllListAsync()
        {
            var teams = await _teamRepository.GetAll().ProjectTo<TeamListMV>(_mapper.ConfigurationProvider).ToListAsync();
            return teams;
        }

        public async Task<TeamUpdateMV?> GetByIdAsync(int id)
        {
            var team = await _teamRepository.Where(x => x.Id == id)
                .ProjectTo<TeamUpdateMV>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return team;
        }

        public async Task AddTeamAsync(TeamAddMV addMV)
        {
            var team = _mapper.Map<Team>(addMV);
            await _teamRepository.AddAsync(team);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateTeamAsync(TeamUpdateMV updateMV)
        {
            var team = _mapper.Map<Team>(updateMV);
            _teamRepository.Update(team);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteTeamAsync(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id);
            _teamRepository.Delete(team);
            await _unitOfWork.CommitAsync();
        }
    }
}