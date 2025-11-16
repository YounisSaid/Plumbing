using EntityLayer.WebApp.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace RepositoryLayer.Configurations
{
    public class TestimonialConfig : BaseConfig<Testimonial>
    {
        public override void Configure(EntityTypeBuilder<Testimonial> builder)
        {
            builder.Property(t => t.Comment).IsRequired().HasMaxLength(2000);
            builder.Property(t => t.FullName).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Title).IsRequired().HasMaxLength(100);
            builder.Property(t => t.FileType).IsRequired();
            builder.Property(t => t.FileName).IsRequired();
            builder.HasData(new Testimonial
            {
                Id = 1,
                Comment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. In nibh mauris cursus mattis molestie a iaculis at erat. Odio ut enim blandit volutpat maecenas volutpat blandit aliquam etiam.",
                Title = "interesting",
                FullName = "Merlyn Monroe",
                FileName = "test",
                FileType = "test",
                CreatedAt = DateTime.Now.ToString("d")
            }, new Testimonial
            {
                Id = 2,
                Comment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. In nibh mauris cursus mattis molestie a iaculis at erat. Odio ut enim blandit volutpat maecenas volutpat blandit aliquam etiam.",
                Title = "interesting",
                FullName = "Jackie Chan",
                FileName = "test",
                FileType = "test",
                CreatedAt = DateTime.Now.ToString("d")
            }, new Testimonial
            {
                Id = 3,
                Comment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. In nibh mauris cursus mattis molestie a iaculis at erat. Odio ut enim blandit volutpat maecenas volutpat blandit aliquam etiam.",
                Title = "interesting",
                FullName = "Bruce Wills",
                FileName = "test",
                FileType = "test",
            });
            base.Configure(builder);
        }
    }
}
