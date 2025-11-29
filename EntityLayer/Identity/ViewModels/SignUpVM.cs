namespace EntityLayer.Identity.ViewModels
{
    public class SignUpVM
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool TermsAndConditions { get; set; }

    }
}
