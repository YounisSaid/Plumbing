using EntityLayer.WebApp.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configurations
{
    public class ContactConfig : BaseConfig<Contact>
    {
        public override void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.Property(c => c.Location).IsRequired().HasMaxLength(150);
            builder.Property(c => c.Email).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Call).IsRequired().HasMaxLength(11);
            builder.Property(c => c.Map).IsRequired().HasMaxLength(300);
            builder.HasData(new Contact
            {
                Id = 1,
                Call = "01012365898",
                Email = "test@try.com",
                Location = "Iron streen, Brave Avenue, KD1 2CF, London",
                Map = "TestLink Here",

            });
            base.Configure(builder);
        }
    }
}
