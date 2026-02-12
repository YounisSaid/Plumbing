using AutoMapper;
using EntityLayer.WebApplication.Entites;
using EntityLayer.WebApplication.ViewModels.HomePage;

namespace ServiceLayer.AutoMapper.WebApplication
{
    public class HomePageMapper : Profile
    {
        public HomePageMapper()
        {
            CreateMap<HomePage, HomePageAddMV>().ReverseMap();
            CreateMap<HomePage, HomePageUpdateMV>().ReverseMap();
            CreateMap<HomePage, HomePageListMV>().ReverseMap();
            CreateMap<HomePage, HomePageListMVForUi>().ReverseMap();
        }


    }
}