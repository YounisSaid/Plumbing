using EntityLayer.WebApp.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace RepositoryLayer.Configurations
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

            });
            base.Configure(builder);
        }
    }
}
