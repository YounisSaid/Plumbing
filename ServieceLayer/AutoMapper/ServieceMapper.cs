using AutoMapper;
using EntityLayer.WebApp.Entites;
using EntityLayer.WebApp.ViewModels.Service;

namespace ServieceLayer.AutoMapper
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