using Microsoft.AspNetCore.Identity;

namespace EntityLayer.Identity.Entites
{
    public class AppUser : IdentityUser
    {
        public string? FileName { get; set; }
        public string? FileType { get; set; }
    }
}
