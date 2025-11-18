using AutoMapper;
using EntityLayer.WebApp.Entites;
using EntityLayer.WebApp.ViewModels.SocialMedia;

namespace ServieceLayer.AutoMapper
{
    public class SocialMediaMapper : Profile
    {
        public SocialMediaMapper()
        {
            CreateMap<SocialMedia,SocialMediaAddMV>().ReverseMap();
            CreateMap<SocialMedia,SocialMediaUpdateMV>().ReverseMap();
            CreateMap<SocialMedia,SocialMediaListMV>().ReverseMap();
        }


    }
}