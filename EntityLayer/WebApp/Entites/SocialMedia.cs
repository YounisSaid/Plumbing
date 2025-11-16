using CoreLayer.BaseEntities;

namespace EntityLayer.WebApp.Entites
{
    public class SocialMedia : BaseEntity
    {
        public string? Twitter { get; set; }
        public string? Facebook { get; set; }
        public string? LinkedIn { get; set; }
        public string? Instagram { get; set; }

        public About About { get; set; } = null!;
    }
}
