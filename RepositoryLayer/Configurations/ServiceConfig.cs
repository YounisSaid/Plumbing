using EntityLayer.WebApp.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace RepositoryLayer.Configurations
{
    public class ServiceConfig : BaseConfig<Service>
    {
        public override void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Description).IsRequired().HasMaxLength(2000);
            builder.Property(s => s.Icon).IsRequired().HasMaxLength(200);
            builder.HasData(new Service
            {
                Id = 1,
                Icon = "bi bi-service1",
                Name = "Plumbing Service 1",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. In nibh mauris cursus mattis molestie a iaculis at erat. Odio ut enim blandit volutpat maecenas volutpat blandit aliquam etiam."

            }, new Service
            {
                Id = 2,
                Icon = "bi bi-service2",
                Name = "Plumbing Service 2",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. In nibh mauris cursus mattis molestie a iaculis at erat. Odio ut enim blandit volutpat maecenas volutpat blandit aliquam etiam."

            }, new Service
            {
                Id = 3,
                Icon = "bi bi-service3",
                Name = "Plumbing Service 3",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. In nibh mauris cursus mattis molestie a iaculis at erat. Odio ut enim blandit volutpat maecenas volutpat blandit aliquam etiam."

            });
            base.Configure(builder);
        }
    }
}
