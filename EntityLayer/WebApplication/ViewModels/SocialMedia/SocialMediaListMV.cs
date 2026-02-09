using EntityLayer.WebApplication.ViewModels.About;

namespace EntityLayer.WebApplication.ViewModels.SocialMedia
{
    public class SocialMediaListMV
    {
        public virtual int Id { get; set; }
        public virtual string CreatedAt { get; set; } = null!;
        public virtual string? UpdatedAt { get; set; } = null;

        public string? Twitter { get; set; }
        public string? Facebook { get; set; }
        public string? LinkedIn { get; set; }
        public string? Instagram { get; set; }

        public AboutListMV About { get; set; } = null!;
    }
}
