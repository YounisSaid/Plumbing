using AutoMapper;
using EntityLayer.WebApplication.Entites;
using EntityLayer.WebApplication.ViewModels.SocialMedia;

namespace ServiceLayer.AutoMapper.WebApplication
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