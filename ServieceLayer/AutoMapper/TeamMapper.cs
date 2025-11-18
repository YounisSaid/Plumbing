using AutoMapper;
using EntityLayer.WebApp.Entites;
using EntityLayer.WebApp.ViewModels.Team;

namespace ServieceLayer.AutoMapper
{
    public class TeamMapper : Profile
    {
        public TeamMapper()
        {
            CreateMap<Team,TeamAddMV>().ReverseMap();
            CreateMap<Team,TeamUpdateMV>().ReverseMap();
            CreateMap<Team,TeamListMV>().ReverseMap();
        }


    }
}