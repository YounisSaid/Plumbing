using EntityLayer.WebApplication.ViewModels.Category;
using Microsoft.AspNetCore.Http;

namespace EntityLayer.WebApplication.ViewModels.Portfolio
{
    public class PortfolioAddMV
    {
        public string Title { get; set; } = null!;
        public string FileType { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public IFormFile Photo { get; set; } = null!;

        public int CategoryId { get; set; }
        public CategoryAddMV Category { get; set; } = null!;
        public IList<CategoryListMV> CategoryList { get; set; } = null!;
    }
}
