namespace EntityLayer.WebApp.ViewModels.Service
{
    public class ServiceUpdateMV
    {
        public int Id { get; set; }
        public string? UpdatedAt { get; set; }
        public byte[] RowVersion { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Icon { get; set; } = null!;
    }
}
