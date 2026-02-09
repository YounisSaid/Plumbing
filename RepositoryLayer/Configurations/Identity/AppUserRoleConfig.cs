using EntityLayer.Identity.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configurations.Identity
{
    public class AppUserRoleConfig : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            builder.HasData(new AppUserRole
            {
                RoleId = "40C9B336-098C-4B39-B039-CB6E5A66803D",
                UserId = "B1D61281-1273-4F2F-867F-BE9ADE0377A6"
            },
            new AppUserRole
            {
                RoleId = "70552DF1-CB86-4D03-89C3-3DC76CC5B580",
                UserId = "77EBB6A6-7426-4C99-9A5B-F1975438F764"
            });

        }
    }
}
