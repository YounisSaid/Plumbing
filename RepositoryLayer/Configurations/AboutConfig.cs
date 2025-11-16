using EntityLayer.WebApp.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configurations
{
    public class AboutConfig : BaseConfig<About>
    {
        public override void Configure(EntityTypeBuilder<About> builder)
        {
            builder.Property(a => a.Header).IsRequired().HasMaxLength(200);
            builder.Property(a => a.Description).IsRequired().HasMaxLength(2000);
            builder.Property(a => a.Clients).IsRequired().HasMaxLength(5);
            builder.Property(a => a.Projects).IsRequired().HasMaxLength(5);
            builder.Property(a => a.HoursOfSupport).IsRequired().HasMaxLength(5);
            builder.Property(a => a.HardWorkers).IsRequired().HasMaxLength(5);
            builder.Property(a => a.FileType).IsRequired();
            builder.Property(a => a.FileName).IsRequired();

            builder.HasData(new About
            {
                Id = 1,
                Header = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. In nibh mauris cursus mattis molestie a iaculis at erat. Odio ut enim blandit volutpat maecenas volutpat blandit aliquam etiam. Suspendisse sed nisi lacus sed viverra tellus in. Amet volutpat consequat mauris nunc congue. Diam maecenas sed enim ut sem. Et magnis dis parturient montes nascetur. Morbi tempus iaculis urna id volutpat lacus laoreet. Urna condimentum mattis pellentesque id nibh tortor id. Fames ac turpis egestas maecenas pharetra convallis posuere. Nunc mi ipsum faucibus vitae aliquet.",
                Clients = 5,
                Projects = 5,
                HoursOfSupport = 150,
                HardWorkers = 3,
                FileName = "test",
                FileType = "test",
                SocialMediaID = 1,
            });
            base.Configure(builder);
        }
    }
}
