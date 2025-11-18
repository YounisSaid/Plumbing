namespace EntityLayer.WebApp.ViewModels.Category
{
    public class CategoryListMV
    {
        public int Id { get; set; }
        public string CreatedAt { get; set; } = DateTime.Now.ToString("d");
        public string? UpdatedAt { get; set; }
        public string Name { get; set; } = null!;
    }
}
