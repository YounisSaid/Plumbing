using AutoMapper;
using EntityLayer.WebApp.Entites;
using EntityLayer.WebApp.ViewModels.Portfolio;

namespace ServieceLayer.AutoMapper
{
    public class PortofolioMapper : Profile
    {
        public PortofolioMapper()
        {
            CreateMap<Portfolio,PortfolioAddMV>().ReverseMap();
            CreateMap<Portfolio,PortfolioUpdateMV>().ReverseMap();
            CreateMap<Portfolio,PortfolioListMV>().ReverseMap();
        }


    }
}