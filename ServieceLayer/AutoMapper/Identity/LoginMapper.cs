using AutoMapper;
using EntityLayer.Identity.Entites;
using EntityLayer.Identity.ViewModels;

namespace ServiceLayer.AutoMapper.Identity
{
    public class LoginMapper : Profile
    {
        public LoginMapper()
        {
            CreateMap<AppUser, LoginMV>().ReverseMap();
        }
    }
}
