using EntityLayer.WebApp.ViewModels.SocialMedia;

namespace EntityLayer.WebApp.ViewModels.About
{
    public class AboutListMV
    {
        public int Id { get; set; }
        public  string CreatedAt { get; set; } = DateTime.Now.ToString("d");
        public  string? UpdatedAt { get; set; } 
        public string Header { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Clients { get; set; }
        public int Projects { get; set; }
        public int HoursOfSupport { get; set; }
        public int HardWorkers { get; set; }
        public string FileType { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public int SocialMediaID { get; set; }
        public SocialMediaListMV SocialMedia { get; set; } = null!;
    }
}
