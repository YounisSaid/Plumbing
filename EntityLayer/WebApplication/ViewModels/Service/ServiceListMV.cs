namespace EntityLayer.WebApplication.ViewModels.Service
{
    public class ServiceListMV
    {
        public virtual int Id { get; set; }
        public virtual string CreatedAt { get; set; } = null!;
        public virtual string? UpdatedAt { get; set; } = null;

        public string Name { get; set; } = null!;

    }
}
