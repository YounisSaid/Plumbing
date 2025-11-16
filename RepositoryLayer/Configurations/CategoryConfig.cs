using EntityLayer.WebApp.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configurations
{
    public class CategoryConfig : BaseConfig<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.HasData(new Category
            {
                Id = 1,
                Name = "Projects",
            }, new Category
            {
                Id = 2,
                Name = "SiteWorks",
            });

            base.Configure(builder);
        }
    }
}
