using EntityLayer.Identity.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configurations.Identity
{
    public class AppRoleConfig : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            var superAdmin = new AppRole
            {
                Id = "40C9B336-098C-4B39-B039-CB6E5A66803D",
                Name = "SuperAdmin",
                NormalizedName = "SUPERADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            builder.HasData(superAdmin);

            var member = new AppRole
            {
                Id = "70552DF1-CB86-4D03-89C3-3DC76CC5B580",
                Name = "Member",
                NormalizedName = "MEMBER",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            builder.HasData(member);
        }
    }
}
