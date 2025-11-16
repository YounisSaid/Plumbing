using CoreLayer.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configurations
{
    public abstract class BaseConfig<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            
            builder.Property(e => e.CreatedAt).IsRequired().HasMaxLength(10);
            builder.Property(e => e.UpdatedAt).HasMaxLength(10);
            builder.Property(e => e.RowVersion).IsRequired().IsRowVersion();
        }
    }
}
