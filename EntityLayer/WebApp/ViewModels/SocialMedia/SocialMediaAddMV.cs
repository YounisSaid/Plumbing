using EntityLayer.WebApp.ViewModels.About;

namespace EntityLayer.WebApp.ViewModels.SocialMedia
{
    public class SocialMediaAddMV
    {
        public string? Twitter { get; set; }
        public string? Facebook { get; set; }
        public string? LinkedIn { get; set; }
        public string? Instagram { get; set; }

        public AboutAddVM About { get; set; } = null!;
    }
}
