using AutoMapper;
using EntityLayer.Identity.Entites;
using EntityLayer.Identity.ViewModels;

namespace ServiceLayer.AutoMapper.Identity
{
    public class UserEditMapper : Profile
    {
        public UserEditMapper()
        {
            CreateMap<AppUser, UserEditMV>().ReverseMap();
        }
    }
}
