using EntityLayer.WebApp.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configurations
{
    public class PortofolioConfig : BaseConfig<Portfolio>
    {
        public override void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            builder.Property(p => p.Title).IsRequired().HasMaxLength(100);
            builder.Property(p => p.FileType).IsRequired();
            builder.Property(p => p.FileName).IsRequired();
            builder.HasData(new Portfolio
            {
                Id = 1,
                CategoryId = 1,
                FileName = "Test",
                FileType = "test",
                Title = "Test Picture",

            }, new Portfolio
            {
                Id = 2,
                CategoryId = 1,
                FileName = "Test2",
                FileType = "test2",
                Title = "Test Picture2",

            }, new Portfolio
            {
                Id = 3,
                CategoryId = 2,
                FileName = "Test3",
                FileType = "test3",
                Title = "Test Picture3",

            }, new Portfolio
            {
                Id = 4,
                CategoryId = 2,
                FileName = "Test4",
                FileType = "test4",
                Title = "Test Picture4",

            });

            base.Configure(builder);
        }
    }
}
