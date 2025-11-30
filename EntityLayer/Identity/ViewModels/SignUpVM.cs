namespace EntityLayer.Identity.ViewModels
{
    public class SignUpVM
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
        public bool TermsAndConditions { get; set; }

    }
}
