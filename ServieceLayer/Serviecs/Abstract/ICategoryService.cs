using EntityLayer.WebApp.ViewModels.Category;

namespace ServieceLayer.Serviecs.Abstract
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