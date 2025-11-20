using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApp.Entites;
using EntityLayer.WebApp.ViewModels.Category;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServieceLayer.Serviecs.Abstract;

namespace ServieceLayer.Serviecs.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Category> _categoryRepository;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _categoryRepository = _unitOfWork.GetRepository<Category>();
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
        }

        public async Task UpdateCategoryAsync(CategoryUpdateMV updateMV)
        {
            var category = _mapper.Map<Category>(updateMV);
            _categoryRepository.Update(category);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            _categoryRepository.Delete(category);
            await _unitOfWork.CommitAsync();
        }
    }
}