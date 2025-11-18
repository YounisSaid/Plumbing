using EntityLayer.WebApp.ViewModels.Category;

namespace EntityLayer.WebApp.ViewModels.Portfolio
{
    public class PortfolioAddMV
    {
        public string Title { get; set; } = null!;
        public string FileType { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public int CategoryId { get; set; }
        public CategoryAddMV Category { get; set; } = null!;
    }
}
