using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.Enumerates;
using EntityLayer.WebApplication.Entites;
using EntityLayer.WebApplication.ViewModels.Testimonial;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Helpers.Generic;
using ServiceLayer.Serviecs.WebApplication.Abstract;

namespace ServiceLayer.Serviecs.WebApplication.Concrete
{
    public class TestimonialService : ITestimonialService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Testimonial> _testimonialRepository;
        public readonly IImageHelper _imageHelper;


        public TestimonialService(IUnitOfWork unitOfWork, IMapper mapper, IImageHelper imageHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _testimonialRepository = _unitOfWork.GetRepository<Testimonial>();
            _imageHelper = imageHelper;
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
            var image = await _imageHelper.UploadImageAsync(null, addMV.Photo, imageType.testimonials);
            if (image.Error != null)
            {
                return;
            }
            addMV.FileName = image.FileName!;
            addMV.FileType = image.FileType!;
            var testimonial = _mapper.Map<Testimonial>(addMV);
            await _testimonialRepository.AddAsync(testimonial);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateTestimonialAsync(TestimonialUpdateMV updateMV)
        {
            var oldpTestimonial = await _testimonialRepository.Where(x => x.Id == updateMV.Id).AsNoTracking().FirstAsync();
            if (updateMV.Photo != null)
            {
                var image = await _imageHelper.UploadImageAsync(null, updateMV.Photo, imageType.about);
                if (image.Error != null)
                {
                    return;
                }
                updateMV.FileName = image.FileName!;
                updateMV.FileType = image.FileType!;
            }
            var testimonial = _mapper.Map<Testimonial>(updateMV);
            _testimonialRepository.Update(testimonial);
            await _unitOfWork.CommitAsync();

            if (updateMV.Photo != null)
            {
                _imageHelper.DeleteImage(oldpTestimonial!.FileName);
            }
        }

        public async Task DeleteTestimonialAsync(int id)
        {
            var testimonial = await _testimonialRepository.GetByIdAsync(id);
            _testimonialRepository.Delete(testimonial!);
            await _unitOfWork.CommitAsync();
        }
    }
}