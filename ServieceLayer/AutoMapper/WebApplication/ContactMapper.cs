using AutoMapper;
using EntityLayer.WebApplication.Entites;
using EntityLayer.WebApplication.ViewModels.Contact;

namespace ServiceLayer.AutoMapper.WebApplication
{
    public class ContactMapper : Profile
    {
        public ContactMapper()
        {
            CreateMap<Contact, ContactAddMV>().ReverseMap();
            CreateMap<Contact, ContactUpdateMV>().ReverseMap();
            CreateMap<Contact, ContactListMV>().ReverseMap();
            CreateMap<Contact, ContactListMVForUi>().ReverseMap();
        }


    }
}