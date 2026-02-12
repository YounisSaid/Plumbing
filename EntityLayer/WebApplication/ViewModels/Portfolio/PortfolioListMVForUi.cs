using EntityLayer.WebApplication.ViewModels.Category;

namespace EntityLayer.WebApplication.ViewModels.Portfolio
{
    public class PortfolioListMVForUi
    {
        public string Title { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public CategoryListMVForUi Category { get; set; } = null!;
    }
}
