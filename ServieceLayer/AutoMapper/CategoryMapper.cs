using AutoMapper;
using EntityLayer.WebApp.Entites;
using EntityLayer.WebApp.ViewModels.Category;

namespace ServieceLayer.AutoMapper
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<Category, CategoryAddMV>().ReverseMap();
            CreateMap<Category, CategoryUpdateMV>().ReverseMap();
            CreateMap<Category, CategoryListMV>().ReverseMap();
        }


    }
}