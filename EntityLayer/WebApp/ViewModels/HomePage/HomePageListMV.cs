namespace EntityLayer.WebApp.ViewModels.HomePage
{
    public class HomePageListMV
    {
        public virtual int Id { get; set; }
        public virtual string CreatedAt { get; set; } = DateTime.Now.ToString("d");
        public virtual string? UpdatedAt { get; set; } = null;

        public string Header { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string VideoLink { get; set; } = null!;

    }
}
