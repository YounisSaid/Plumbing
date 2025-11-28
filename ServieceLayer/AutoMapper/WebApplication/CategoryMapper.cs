using AutoMapper;
using EntityLayer.WebApplication.Entites;
using EntityLayer.WebApplication.ViewModels.Category;

namespace ServiceLayer.AutoMapper.WebApplication
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