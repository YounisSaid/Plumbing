using EntityLayer.WebApp.ViewModels.Category;

namespace EntityLayer.WebApp.ViewModels.Portfolio
{
    public class PortfolioUpdateMV
    {
        public int Id { get; set; }
        public string? UpdatedAt { get; set; }
        public byte[] RowVersion { get; set; } = null!;

        public string Title { get; set; } = null!;
        public string FileType { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public int CategoryId { get; set; }
        public CategoryUpdateMV Category { get; set; } = null!;
    }
}
