namespace ServiceLayer.Messages.Identity
{
    public static class IdentityValidationMessages
    {
        public const string SecurityStampError = "Your Critical Information Has Been Changed ,Please Try To Login!!";
        public static string CheckEmail()
        {
            return "Value Should be in Email Format";
        }

        public static string ComaprePassword()
        {
            return "Password and Confirm Password must be the same";
        }

    }
}
