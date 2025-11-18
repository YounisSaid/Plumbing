using EntityLayer.WebApp.ViewModels.About;

namespace EntityLayer.WebApp.ViewModels.SocialMedia
{
    public class SocialMediaListMV
    {
        public virtual int Id { get; set; }
        public virtual string CreatedAt { get; set; } = DateTime.Now.ToString("d");
        public virtual string? UpdatedAt { get; set; } = null;

        public string? Twitter { get; set; }
        public string? Facebook { get; set; }
        public string? LinkedIn { get; set; }
        public string? Instagram { get; set; }

        public AboutListMV About { get; set; } = null!;
    }
}
