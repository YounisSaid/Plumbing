namespace EntityLayer.Identity.ViewModels
{
    public class LoginMV
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; }
    }
}
