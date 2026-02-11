using EntityLayer.Identity.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configurations.Identity
{
    public class AppUserClaimConfig : IEntityTypeConfiguration<AppUserClaim>
    {
        public void Configure(EntityTypeBuilder<AppUserClaim> builder)
        {
            builder.HasData(new AppUserClaim
            {
                Id = 1,
                UserId = "77EBB6A6-7426-4C99-9A5B-F1975438F764",
                ClaimType = "AdminObserverExpireDate",
                ClaimValue = "12/2/2026"
            });

        }
    }
}
