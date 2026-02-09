using EntityLayer.Identity.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configurations.Identity
{
    public class AppUserConfig : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            var superAdmin = new AppUser
            {
                Id = "B1D61281-1273-4F2F-867F-BE9ADE0377A6",
                UserName = "YounisSaid",
                NormalizedUserName = "YOUNISSAID",
                Email = "test.plumbing.mvc@gmail.com",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                SecurityStamp = Guid.NewGuid().ToString()
            };
            string superAdminHashedPassword = PasswordHasher(superAdmin, "P@ssw0rd12");
            superAdmin.PasswordHash = superAdminHashedPassword;
            builder.HasData(superAdmin);

            var member = new AppUser
            {
                Id = "77EBB6A6-7426-4C99-9A5B-F1975438F764",
                UserName = "YoussefRamadan",
                NormalizedUserName = "YOUSSEFRAMADAN",
                Email = "test.plumbing.mvc1@gmail.com",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                SecurityStamp = Guid.NewGuid().ToString()
            };
            string memberHashedPassword = PasswordHasher(member, "P@ssw0rd12");
            member.PasswordHash = memberHashedPassword;
            builder.HasData(member);
        }

        private string PasswordHasher(AppUser appUser, string password)
        {
            var passwordHasher = new PasswordHasher<AppUser>();
            return passwordHasher.HashPassword(appUser, password);
        }
    }
}
