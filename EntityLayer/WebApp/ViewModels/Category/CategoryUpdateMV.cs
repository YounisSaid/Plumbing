namespace EntityLayer.WebApp.ViewModels.Category
{
    public class CategoryUpdateMV
    {
        public int Id { get; set; }
        public string? UpdatedAt { get; set; }
        public byte[] RowVersion { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
