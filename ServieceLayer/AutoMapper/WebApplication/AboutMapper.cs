using AutoMapper;
using EntityLayer.WebApplication.Entites;
using EntityLayer.WebApplication.ViewModels.About;

namespace ServiceLayer.AutoMapper.WebApplication
{
    public class AboutMapper : Profile
    {
        public AboutMapper()
        {
            CreateMap<About, AboutAddVM>().ReverseMap();
            CreateMap<About, AboutUpdateVM>().ReverseMap();
            CreateMap<About, AboutListMV>().ReverseMap();
            CreateMap<About, AboutListMVForUi>().ReverseMap();

        }


    }
}
