using EntityLayer.WebApp.ViewModels.SocialMedia;
using Microsoft.AspNetCore.Http;

namespace EntityLayer.WebApp.ViewModels.About
{
    public class AboutUpdateVM
    {
        public int Id { get; set; }
        public string? UpdatedAt { get; set; }
        public byte[] RowVersion { get; set; } = null!;
        public string Header { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Clients { get; set; }
        public int Projects { get; set; }
        public int HoursOfSupport { get; set; }
        public int HardWorkers { get; set; }
        public string FileType { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public IFormFile Photo { get; set; } = null!;

        public int SocialMediaID { get; set; }
        public SocialMediaUpdateMV SocialMedia { get; set; } = null!;
    }
}
