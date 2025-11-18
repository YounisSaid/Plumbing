namespace EntityLayer.WebApp.ViewModels.Service
{
    public class ServiceListMV
    {
        public virtual int Id { get; set; }
        public virtual string CreatedAt { get; set; } = DateTime.Now.ToString("d");
        public virtual string? UpdatedAt { get; set; } = null;

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Icon { get; set; } = null!;
    }
}
