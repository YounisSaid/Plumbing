using AutoMapper;
using EntityLayer.WebApp.Entites;
using EntityLayer.WebApp.ViewModels.HomePage;

namespace ServieceLayer.AutoMapper
{
    public class HomePageMapper : Profile
    {
        public HomePageMapper()
        {
            CreateMap<HomePage,HomePageAddMV>().ReverseMap();
            CreateMap<HomePage,HomePageUpdateMV>().ReverseMap();
            CreateMap<HomePage,HomePageListMV>().ReverseMap();
        }


    }
}