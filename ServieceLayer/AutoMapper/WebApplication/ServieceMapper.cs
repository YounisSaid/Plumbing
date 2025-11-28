using AutoMapper;
using EntityLayer.WebApp.Entites;
using EntityLayer.WebApp.ViewModels.Service;

namespace ServieceLayer.AutoMapper.WebApplication
{
    public class ServieceMapper : Profile
    {
        public ServieceMapper()
        {
            CreateMap<Service,ServiceAddMV>().ReverseMap();
            CreateMap<Service,ServiceUpdateMV>().ReverseMap();
            CreateMap<Service,ServiceListMV>().ReverseMap();
        }


    }
}