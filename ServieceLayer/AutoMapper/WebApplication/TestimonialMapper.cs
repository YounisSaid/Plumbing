using AutoMapper;
using EntityLayer.WebApplication.Entites;
using EntityLayer.WebApplication.ViewModels.Testimonial;

namespace ServiceLayer.AutoMapper.WebApplication
{
    public class TestimonialMapper : Profile
    {
        public TestimonialMapper()
        {
            CreateMap<Testimonial, TestimonialAddMV>().ReverseMap();
            CreateMap<Testimonial, TestimonialUpdateMV>().ReverseMap();
            CreateMap<Testimonial, TestimonialListMV>().ReverseMap();
            CreateMap<Testimonial, TestimonialListMVForUi>().ReverseMap();
        }


    }
}