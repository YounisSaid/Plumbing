namespace EntityLayer.WebApp.ViewModels.Testimonial
{
    public class TestimonialListMV
    {
        public virtual int Id { get; set; }
        public virtual string CreatedAt { get; set; } = DateTime.Now.ToString("d");
        public virtual string? UpdatedAt { get; set; } = null;
        public string Comment { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string FileType { get; set; } = null!;
        public string FileName { get; set; } = null!;
    }
}
