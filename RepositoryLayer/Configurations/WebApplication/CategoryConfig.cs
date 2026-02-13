using EntityLayer.WebApplication.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configurations.WebApplication
{
    public class CategoryConfig : BaseConfig<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.HasMany(c => c.Portofolios).WithOne(c => c.Category).OnDelete(DeleteBehavior.Restrict);
            //builder.HasData(new Category
            //{
            //    Id = 1,
            //    Name = "Projects",
            //    CreatedAt = "2/9/2025"
            //}, new Category
            //{
            //    Id = 2,
            //    Name = "SiteWorks",
            //    CreatedAt = "2/9/2025"
            //});

            base.Configure(builder);
        }
    }
}
