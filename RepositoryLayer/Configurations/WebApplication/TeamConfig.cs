using EntityLayer.WebApplication.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace RepositoryLayer.Configurations.WebApplication
{
    public class TeamConfig : BaseConfig<Team>
    {
        public override void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.Property(t => t.FullName).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Title).IsRequired().HasMaxLength(100);
            builder.Property(t => t.FileType).IsRequired();
            builder.Property(t => t.FileName).IsRequired();
            builder.HasData(new Team
            {
                Id = 1,
                FullName = "John Black",
                Title = "Professor",
                Facebook = "facebook",
                Instagram = "instagram",
                FileName = "test",
                FileType = "test",
                CreatedAt = "2/9/2025"

            });
            base.Configure(builder);
        }
    }
}
