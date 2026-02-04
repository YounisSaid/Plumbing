using AutoMapper;
using EntityLayer.WebApplication.Entites;
using EntityLayer.WebApplication.ViewModels.Service;

namespace ServiceLayer.AutoMapper.WebApplication
{
    public class ServiceMapper : Profile
    {
        public ServiceMapper()
        {
            CreateMap<Service,ServiceAddMV>().ReverseMap();
            CreateMap<Service,ServiceUpdateMV>().ReverseMap();
            CreateMap<Service,ServiceListMV>().ReverseMap();
        }


    }
}