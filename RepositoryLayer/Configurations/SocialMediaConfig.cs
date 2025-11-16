using EntityLayer.WebApp.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace RepositoryLayer.Configurations
{
    public class SocialMediaConfig : BaseConfig<SocialMedia>
    {
        public override void Configure(EntityTypeBuilder<SocialMedia> builder)
        {
            builder.HasData(new SocialMedia
            {
                Id = 1,
                Facebook = "testFacebook",
                Instagram = "testInstagram",

            });
            base.Configure(builder);
        }
    }
}
