using AutoMapper;
using EntityLayer.Identity.Entites;
using EntityLayer.Identity.ViewModels;

namespace ServiceLayer.AutoMapper.Identity
{
    public class ResetPasswordMapper : Profile
    {
        public ResetPasswordMapper()
        {
            CreateMap<AppUser,ResetPasswordVM>().ReverseMap();
        }
    }
}
