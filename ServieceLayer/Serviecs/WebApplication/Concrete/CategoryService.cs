using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApplication.Entites;
using EntityLayer.WebApplication.ViewModels.Category;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Messages.WebApplication;
using ServiceLayer.Serviecs.WebApplication.Abstract;

namespace ServiceLayer.Serviecs.WebApplication.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IToastNotification _toasty;
        private const string Section = "Category Section";
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, IToastNotification toasty)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _categoryRepository = _unitOfWork.GetRepository<Category>();
            _toasty = toasty;
        }

        public async Task<List<CategoryListMV>> GetAllListAsync()
        {
            var categories = await _categoryRepository.GetAll().ProjectTo<CategoryListMV>(_mapper.ConfigurationProvider).ToListAsync();
            return categories;
        }

        public async Task<CategoryUpdateMV?> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.Where(x => x.Id == id)
                .ProjectTo<CategoryUpdateMV>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return category;
        }

        public async Task AddCategoryAsync(CategoryAddMV addMV)
        {
            var category = _mapper.Map<Category>(addMV);
            await _categoryRepository.AddAsync(category);
            await _unitOfWork.CommitAsync();

            _toasty.AddSuccessToastMessage(NotificationMessagesWebApplication.AddMessage(Section), new ToastrOptions { Title = NotificationMessagesWebApplication.SuccessedTitle });

        }

        public async Task UpdateCategoryAsync(CategoryUpdateMV updateMV)
        {
            var category = _mapper.Map<Category>(updateMV);
            _categoryRepository.Update(category);
            await _unitOfWork.CommitAsync();
            _toasty.AddWarningToastMessage(NotificationMessagesWebApplication.UpdateMessage(Section), new ToastrOptions { Title = NotificationMessagesWebApplication.WarningTitle });

        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            _categoryRepository.Delete(category!);
            await _unitOfWork.CommitAsync();
            _toasty.AddWarningToastMessage(NotificationMessagesWebApplication.DeleteMessage(Section), new ToastrOptions { Title = NotificationMessagesWebApplication.WarningTitle });
        }
    }
}