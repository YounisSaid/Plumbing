using AutoMapper;
using EntityLayer.WebApp.Entites;
using EntityLayer.WebApp.ViewModels.Contact;

namespace ServieceLayer.AutoMapper.WebApplication
{
    public class ContactMapper : Profile
    {
        public ContactMapper()
        {
            CreateMap<Contact,ContactAddMV>().ReverseMap();
            CreateMap<Contact,ContactUpdateMV>().ReverseMap();
            CreateMap<Contact,ContactListMV>().ReverseMap();
        }


    }
}