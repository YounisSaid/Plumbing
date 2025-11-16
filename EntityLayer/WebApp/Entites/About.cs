using CoreLayer.BaseEntities;

namespace EntityLayer.WebApp.Entites
{
    public class About : BaseEntity
    {
        public string Header { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Clients { get; set; }
        public int Projects { get; set; } 
        public int HoursOfSupport { get; set; } 
        public int HardWorkers { get; set; } 
        public string FileType { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public int SocialMediaID { get; set; }
        public SocialMedia SocialMedia { get; set; } = null!;
    }
}
