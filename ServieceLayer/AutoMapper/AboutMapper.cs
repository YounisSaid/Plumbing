using AutoMapper;
using EntityLayer.WebApp.Entites;
using EntityLayer.WebApp.ViewModels.About;

namespace ServieceLayer.AutoMapper
{
    public class AboutMapper : Profile
    {
        public AboutMapper()
        {
              CreateMap<About, AboutAddVM>().ReverseMap();
              CreateMap<About, AboutUpdateVM>().ReverseMap();
              CreateMap<About, AboutListMV>().ReverseMap();

        }

       
    }
}
