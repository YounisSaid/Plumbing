using EntityLayer.WebApplication.ViewModels.Category;

namespace ServiceLayer.Serviecs.WebApplication.Abstract
{
    public interface ICategoryService
    {
        Task<List<CategoryListMV>> GetAllListAsync();
        Task<CategoryUpdateMV?> GetByIdAsync(int id);
        Task AddCategoryAsync(CategoryAddMV addMV);
        Task UpdateCategoryAsync(CategoryUpdateMV updateMV);
        Task DeleteCategoryAsync(int id);
    }
}