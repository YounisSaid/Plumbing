using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.Enumerates;
using EntityLayer.WebApplication.Entites;
using EntityLayer.WebApplication.ViewModels.Testimonial;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Exceptions.WebApplication;
using ServiceLayer.Helpers.Generic;
using ServiceLayer.Messages.WebApplication;
using ServiceLayer.Serviecs.WebApplication.Abstract;

namespace ServiceLayer.Serviecs.WebApplication.Concrete
{
    public class TestimonialService : ITestimonialService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Testimonial> _testimonialRepository;
        public readonly IImageHelper _imageHelper;
        private readonly IToastNotification _toasty;
        private const string Section = "Testimonial Section";



        public TestimonialService(IUnitOfWork unitOfWork, IMapper mapper, IImageHelper imageHelper, IToastNotification toasty)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _testimonialRepository = _unitOfWork.GetRepository<Testimonial>();
            _imageHelper = imageHelper;
            _toasty = toasty;
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
                _toasty.AddErrorToastMessage(image.Error, new ToastrOptions { Title = NotificationMessagesWebApplication.FailedTitle });
                return;
            }
            addMV.FileName = image.FileName!;
            addMV.FileType = image.FileType!;
            var testimonial = _mapper.Map<Testimonial>(addMV);
            await _testimonialRepository.AddAsync(testimonial);
            await _unitOfWork.CommitAsync();
            _toasty.AddSuccessToastMessage(NotificationMessagesWebApplication.AddMessage(Section), new ToastrOptions { Title = NotificationMessagesWebApplication.SuccessedTitle });
        }

        public async Task UpdateTestimonialAsync(TestimonialUpdateMV updateMV)
        {
            var oldpTestimonial = await _testimonialRepository.Where(x => x.Id == updateMV.Id).AsNoTracking().FirstAsync();
            if (updateMV.Photo != null)
            {
                var image = await _imageHelper.UploadImageAsync(null, updateMV.Photo, imageType.about);
                if (image.Error != null)
                {
                    _toasty.AddErrorToastMessage(image.Error, new ToastrOptions { Title = NotificationMessagesWebApplication.FailedTitle });
                    return;
                }
                updateMV.FileName = image.FileName!;
                updateMV.FileType = image.FileType!;
            }
            else
            {
                updateMV.FileName = oldpTestimonial.FileName;
                updateMV.FileType = oldpTestimonial.FileType;
            }
            var testimonial = _mapper.Map<Testimonial>(updateMV);
            _testimonialRepository.Update(testimonial);
            bool result = await _unitOfWork.CommitAsync();
            if (!result)
            {
                _imageHelper.DeleteImage(updateMV.FileName);
                throw new ClientSideException(ExceptionMessages.ConcurrencyException);
            }


            if (updateMV.Photo != null)
            {
                _imageHelper.DeleteImage(oldpTestimonial!.FileName);
            }
            _toasty.AddWarningToastMessage(NotificationMessagesWebApplication.UpdateMessage(Section), new ToastrOptions { Title = NotificationMessagesWebApplication.WarningTitle });
        }

        public async Task DeleteTestimonialAsync(int id)
        {
            var testimonial = await _testimonialRepository.GetByIdAsync(id);
            _testimonialRepository.Delete(testimonial!);
            await _unitOfWork.CommitAsync();
            _toasty.AddWarningToastMessage(NotificationMessagesWebApplication.DeleteMessage(Section), new ToastrOptions { Title = NotificationMessagesWebApplication.WarningTitle });
        }
    }
}