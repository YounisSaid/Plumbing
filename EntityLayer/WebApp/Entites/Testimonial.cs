using CoreLayer.BaseEntities;

namespace EntityLayer.WebApp.Entites
{
    public class Testimonial : BaseEntity
    {

        public string Comment { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string FileType { get; set; } = null!;
        public string FileName { get; set; } = null!;
    }
}
