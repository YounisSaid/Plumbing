using AutoMapper;
using EntityLayer.WebApp.Entites;
using EntityLayer.WebApp.ViewModels.Testimonial;

namespace ServieceLayer.AutoMapper
{
    public class TestimonialMapper : Profile
    {
        public TestimonialMapper()
        {
            CreateMap<Testimonial,TestimonialAddMV>().ReverseMap();
            CreateMap<Testimonial,TestimonialUpdateMV>().ReverseMap();
            CreateMap<Testimonial,TestimonialListMV>().ReverseMap();
        }


    }
}