using EntityLayer.WebApp.ViewModels.Testimonial;

namespace ServieceLayer.Serviecs.Abstract
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