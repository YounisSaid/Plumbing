namespace EntityLayer.WebApp.ViewModels.Contact
{
    public class ContactListMV
    {
        public virtual int Id { get; set; }

        public virtual string CreatedAt { get; set; } = DateTime.Now.ToString("d");
        public virtual string? UpdatedAt { get; set; } = null;

        public string Location { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Call { get; set; } = null!;
        public string Map { get; set; } = null!;
    }
       
}
