namespace EntityLayer.WebApp.ViewModels.Contact
{
    public class ContactUpdateMV
    {
        public virtual int Id { get; set; }

        public virtual string? UpdatedAt { get; set; } = null;
        public virtual byte[] RowVersion { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Call { get; set; } = null!;
        public string Map { get; set; } = null!;
    }
}
