using AutoMapper;
using EntityLayer.WebApplication.Entites;
using EntityLayer.WebApplication.ViewModels.Portfolio;

namespace ServiceLayer.AutoMapper.WebApplication
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