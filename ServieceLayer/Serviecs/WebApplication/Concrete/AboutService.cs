using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.Enumerates;
using EntityLayer.WebApplication.Entites;
using EntityLayer.WebApplication.ViewModels.About;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Helpers.Generic;
using ServiceLayer.Serviecs.WebApplication.Abstract;

namespace ServiceLayer.Serviecs.WebApplication.Concrete
{
    public class AboutService : IAboutService
    {
        private readonly IUnitOfWork _unitOfWork;  
        private readonly IMapper _mapper;
        private readonly IGenericRepository<About> _aboutRepository;
        private readonly IImageHelper _imageHelper;
        public AboutService(IUnitOfWork unitOfWork, IMapper mapper,IImageHelper imageHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _aboutRepository = _unitOfWork.GetRepository<About>();
            _imageHelper = imageHelper;
        }

        public async Task<List<AboutListMV>> GetAllListAsync()
        {
            var abouts = await _aboutRepository.GetAll().ProjectTo<AboutListMV>(_mapper.ConfigurationProvider).ToListAsync();
            return abouts;
        }

        public async Task<AboutUpdateVM?> GetByIdAsync(int id)
        {
            var about = await _aboutRepository.Where(x => x.Id == id)
                .ProjectTo<AboutUpdateVM>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return about;
        }

        public async Task AddAboutAsync(AboutAddVM addVM)
        {
            var test = await _imageHelper.UploadImage(null, addVM.Photo, imageType.about);
            addVM.FileName = test.FileName!;
            addVM.FileType = test.FileType!;
            var about = _mapper.Map<About>(addVM);
            await _aboutRepository.AddAsync(about);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAboutAsync(AboutUpdateVM updateVM)
        {
            var about = _mapper.Map<About>(updateVM);
            _aboutRepository.Update(about);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAboutAsync(int id)
        {
            var about = await _aboutRepository.GetByIdAsync(id);
             _aboutRepository.Delete(about);
             await _unitOfWork.CommitAsync();
            
        }
    }
}
