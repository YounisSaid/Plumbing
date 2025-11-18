using EntityLayer.WebApp.ViewModels.About;

namespace EntityLayer.WebApp.ViewModels.SocialMedia
{
    public class SocialMediaUpdateMV
    {
        public int Id { get; set; }
        public string? UpdatedAt { get; set; }
        public byte[] RowVersion { get; set; } = null!;

        public string? Twitter { get; set; }
        public string? Facebook { get; set; }
        public string? LinkedIn { get; set; }
        public string? Instagram { get; set; }

        public AboutUpdateVM About { get; set; } = null!;
    }
}
