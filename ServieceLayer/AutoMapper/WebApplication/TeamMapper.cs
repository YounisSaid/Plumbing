using AutoMapper;
using EntityLayer.WebApplication.Entites;
using EntityLayer.WebApplication.ViewModels.Team;

namespace ServiceLayer.AutoMapper.WebApplication
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