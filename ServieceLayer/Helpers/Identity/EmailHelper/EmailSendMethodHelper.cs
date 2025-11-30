namespace ServiceLayer.Helpers.Identity.EmailHelper
{
    public interface IEmailSendMethodHelper
    { 
        Task SendPasswordResetLinkWithToken(string passwordResetLink,string token);
    }

    public class EmailSendMethodHelper : IEmailSendMethodHelper
    {
        public Task SendPasswordResetLinkWithToken(string passwordResetLink, string token)
        {
            throw new NotImplementedException();
        }
    }
}
