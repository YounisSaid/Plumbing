using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.Enumerates;
using EntityLayer.WebApplication.Entites;
using EntityLayer.WebApplication.ViewModels.Team;
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
    public class TeamService : ITeamService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Team> _teamRepository;
        public readonly IImageHelper _imageHelper;
        private readonly IToastNotification _toasty;
        private const string Section = "Team Section";



        public TeamService(IUnitOfWork unitOfWork, IMapper mapper, IImageHelper imageHelper, IToastNotification toasty)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _teamRepository = _unitOfWork.GetRepository<Team>();
            _imageHelper = imageHelper;
            _toasty = toasty;
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
            var image = await _imageHelper.UploadImageAsync(null, addMV.Photo, imageType.team);
            if (image.Error != null)
            {
                _toasty.AddErrorToastMessage(image.Error, new ToastrOptions { Title = NotificationMessagesWebApplication.FailedTitle });
                return;
            }
            addMV.FileName = image.FileName!;
            addMV.FileType = image.FileType!;
            var team = _mapper.Map<Team>(addMV);
            await _teamRepository.AddAsync(team);
            await _unitOfWork.CommitAsync();
            _toasty.AddSuccessToastMessage(NotificationMessagesWebApplication.AddMessage(Section), new ToastrOptions { Title = NotificationMessagesWebApplication.SuccessedTitle });
        }

        public async Task UpdateTeamAsync(TeamUpdateMV updateMV)
        {
            var oldpTeam = await _teamRepository.Where(x => x.Id == updateMV.Id).AsNoTracking().FirstAsync();
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
                updateMV.FileName = oldpTeam.FileName;
                updateMV.FileType = oldpTeam.FileType;
            }
            var team = _mapper.Map<Team>(updateMV);
            _teamRepository.Update(team);
            bool result = await _unitOfWork.CommitAsync();
            if (!result)
            {
                _imageHelper.DeleteImage(updateMV.FileName);
                throw new ClientSideException(ExceptionMessages.ConcurrencyException);
            }

            if (updateMV.Photo != null)
            {
                _imageHelper.DeleteImage(oldpTeam!.FileName);
            }
            _toasty.AddWarningToastMessage(NotificationMessagesWebApplication.UpdateMessage(Section), new ToastrOptions { Title = NotificationMessagesWebApplication.WarningTitle });

        }

        public async Task DeleteTeamAsync(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id);
            _teamRepository.Delete(team!);
            await _unitOfWork.CommitAsync();
            _imageHelper.DeleteImage(team!.FileName);
            _toasty.AddWarningToastMessage(NotificationMessagesWebApplication.DeleteMessage(Section), new ToastrOptions { Title = NotificationMessagesWebApplication.WarningTitle });
        }

        //Services For Ui
        public async Task<List<TeamListMVForUi>> GetAllListForUiAsync()
        {
            var teamsUi = await _teamRepository.GetAll().ProjectTo<TeamListMVForUi>(_mapper.ConfigurationProvider).ToListAsync();
            return teamsUi;
        }
    }
}