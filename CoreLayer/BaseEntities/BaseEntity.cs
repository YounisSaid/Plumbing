namespace CoreLayer.BaseEntities
{
    public abstract class BaseEntity : IBaseEntity
    {
        public virtual int Id { get; set; }

        public virtual string CreatedAt { get; set; } = null!;
        public virtual string? UpdatedAt { get; set; } = null;
        public virtual byte[] RowVersion { get; set; } = null!;
    }
}
