using EntityLayer.WebApp.ViewModels.Category;

namespace EntityLayer.WebApp.ViewModels.Portfolio
{
    public class PortfolioListMV
    {
        public virtual int Id { get; set; }
        public virtual string CreatedAt { get; set; } = DateTime.Now.ToString("d");
        public virtual string? UpdatedAt { get; set; } = null;
        public string Title { get; set; } = null!;
        public string FileType { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public int CategoryId { get; set; }
        public CategoryListMV Category { get; set; } = null!;

    }
}
