using AutoMapper;
using EntityLayer.Identity.Entites;
using EntityLayer.WebApplication.ViewModels.UserList;

namespace ServiceLayer.AutoMapper.WebApplication
{
    public class UserListMapper : Profile
    {
        public UserListMapper()
        {
            CreateMap<UserVM, AppUser>().ReverseMap();
        }


    }
}
