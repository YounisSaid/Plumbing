using EntityLayer.WebApplication.ViewModels.Testimonial;

namespace ServiceLayer.Serviecs.WebApplication.Abstract
{
    public interface ITestimonialService
    {
        Task<List<TestimonialListMV>> GetAllListAsync();
        Task<TestimonialUpdateMV?> GetByIdAsync(int id);
        Task AddTestimonialAsync(TestimonialAddMV addMV);
        Task UpdateTestimonialAsync(TestimonialUpdateMV updateMV);
        Task DeleteTestimonialAsync(int id);
    }
}