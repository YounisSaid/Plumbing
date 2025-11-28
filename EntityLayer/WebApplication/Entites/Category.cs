using CoreLayer.BaseEntities;

namespace EntityLayer.WebApplication.Entites
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = null!;

        public IEnumerable <Portfolio> Portofolios { get; set; } = null!;
    }
}
