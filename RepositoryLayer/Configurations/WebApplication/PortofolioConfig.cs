using EntityLayer.WebApplication.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configurations.WebApplication
{
    public class PortofolioConfig : BaseConfig<Portfolio>
    {
        public override void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            builder.Property(p => p.Title).IsRequired().HasMaxLength(100);
            builder.Property(p => p.FileType).IsRequired();
            builder.Property(p => p.FileName).IsRequired();
            builder.HasOne(p => p.Category).WithMany(p => p.Portofolios).OnDelete(DeleteBehavior.Restrict);
            //builder.HasData(new Portfolio
            //{
            //    Id = 1,
            //    CategoryId = 1,
            //    FileName = "Test",
            //    FileType = "test",
            //    Title = "Test Picture",
            //    CreatedAt = "2/9/2025"

            //}, new Portfolio
            //{
            //    Id = 2,
            //    CategoryId = 1,
            //    FileName = "Test2",
            //    FileType = "test2",
            //    Title = "Test Picture2",
            //    CreatedAt = "2/9/2025"

            //}, new Portfolio
            //{
            //    Id = 3,
            //    CategoryId = 2,
            //    FileName = "Test3",
            //    FileType = "test3",
            //    Title = "Test Picture3",
            //    CreatedAt = "2/9/2025"

            //}, new Portfolio
            //{
            //    Id = 4,
            //    CategoryId = 2,
            //    FileName = "Test4",
            //    FileType = "test4",
            //    Title = "Test Picture4",
            //    CreatedAt = "2/9/2025"

            //});

            base.Configure(builder);
        }
    }
}
