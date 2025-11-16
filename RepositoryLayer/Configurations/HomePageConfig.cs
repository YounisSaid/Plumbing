using EntityLayer.WebApp.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configurations
{
    public class HomePageConfig : BaseConfig<HomePage>
    {
        public override void Configure(EntityTypeBuilder<HomePage> builder)
        {
            builder.Property(hp => hp.Header)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(hp => hp.Description)
                   .IsRequired()
                   .HasMaxLength(2000);
            builder.Property(hp => hp.VideoLink)
                   .IsRequired();
            builder.HasData(new HomePage
            {
                Id = 1,
                Header = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. In nibh mauris cursus mattis molestie a iaculis at erat. Odio ut enim blandit volutpat maecenas volutpat blandit aliquam etiam.",
                VideoLink = "Test Video Link",

            });
            base.Configure(builder);
        }
    }
}
