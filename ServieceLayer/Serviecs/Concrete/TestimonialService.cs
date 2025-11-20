using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApp.Entites;
using EntityLayer.WebApp.ViewModels.Testimonial;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServieceLayer.Serviecs.Abstract;

namespace ServieceLayer.Serviecs.Concrete
{
    public class TestimonialService : ITestimonialService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Testimonial> _testimonialRepository;

        public TestimonialService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _testimonialRepository = _unitOfWork.GetRepository<Testimonial>();
        }

        public async Task<List<TestimonialListMV>> GetAllListAsync()
        {
            var testimonials = await _testimonialRepository.GetAll().ProjectTo<TestimonialListMV>(_mapper.ConfigurationProvider).ToListAsync();
            return testimonials;
        }

        public async Task<TestimonialUpdateMV?> GetByIdAsync(int id)
        {
            var testimonial = await _testimonialRepository.Where(x => x.Id == id)
                .ProjectTo<TestimonialUpdateMV>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return testimonial;
        }

        public async Task AddTestimonialAsync(TestimonialAddMV addMV)
        {
            var testimonial = _mapper.Map<Testimonial>(addMV);
            await _testimonialRepository.AddAsync(testimonial);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateTestimonialAsync(TestimonialUpdateMV updateMV)
        {
            var testimonial = _mapper.Map<Testimonial>(updateMV);
            _testimonialRepository.Update(testimonial);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteTestimonialAsync(int id)
        {
            var testimonial = await _testimonialRepository.GetByIdAsync(id);
            _testimonialRepository.Delete(testimonial);
            await _unitOfWork.CommitAsync();
        }
    }
}