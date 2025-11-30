using AutoMapper;
using EntityLayer.Identity.Entites;
using EntityLayer.Identity.ViewModels;

namespace ServiceLayer.AutoMapper.Identity
{
    public class ForgetPasswordMapper : Profile
    {
        public ForgetPasswordMapper()
        {
            CreateMap<AppUser, ForgetPasswordMV>().ReverseMap();
        }

       
    }
}
