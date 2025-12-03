using EntityLayer.Identity.ViewModels;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace ServiceLayer.Helpers.Identity.EmailHelper
{
    public interface IEmailSendMethodHelper
    {
        Task SendPasswordResetLinkWithToken(string passwordResetLink, string toEmail);
    }

    public class EmailSendMethodHelper : IEmailSendMethodHelper
    {
        private readonly GmailInformationVM _informationVM;
        private const string CompanyName = "Plumbing Company"; 

        public EmailSendMethodHelper(IOptions<GmailInformationVM> informationVM)
        {
            _informationVM = informationVM.Value;
        }

        public async Task SendPasswordResetLinkWithToken(string passwordResetLink, string toEmail)
        {
            var stmpClient = new SmtpClient();
            stmpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            stmpClient.Port = 587;
            stmpClient.Host = _informationVM.Host;
            stmpClient.UseDefaultCredentials = false;
            stmpClient.Credentials = new NetworkCredential(_informationVM.Email, _informationVM.Password);
            stmpClient.EnableSsl = true;

            // --- Dynamic Data Calculation ---
            // The link will expire in 1 hour 
            string expirationTime = "1 hour";
            string currentYear = DateTime.Now.Year.ToString();
            // ---------------------------------

            // The raw HTML template with placeholders
            var emailTemplate = $@"
<!DOCTYPE html>
<html>
<head>
    <title>Password Reset</title>
</head>
<body>
    <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 20px auto; padding: 20px; border: 1px solid #ddd; border-radius: 8px;'>
        <h1 style='color: #333; text-align: center;'>Password Reset Request</h1>
        
        <p style='font-size: 16px; color: #555;'>
            You are receiving this email because we received a password reset request for your account.
        </p>
        
        <div style='text-align: center; margin: 30px 0;'>
            <a href='[RESET_LINK_PLACEHOLDER]' 
               style='background-color: #007bff; color: white; padding: 12px 25px; text-decoration: none; border-radius: 5px; font-weight: bold; font-size: 18px;'
               target='_blank'>
                Reset Password
            </a>
        </div>
        
        <p style='font-size: 14px; color: #777;'>
            If you did not request a password reset, please ignore this email. This link will expire in {expirationTime}.
        </p>
        
        <p style='font-size: 14px; color: #777;'>
            If the button above doesn't work, you can copy and paste the following URL into your web browser:
            <br>
            <a href='[RESET_LINK_PLACEHOLDER]' style='word-break: break-all; color: #007bff;'>
                [RESET_LINK_PLACEHOLDER]
            </a>
        </p>
        
        <hr style='border: none; border-top: 1px solid #eee; margin: 20px 0;'>
        
        <p style='font-size: 12px; color: #aaa; text-align: center;'>
            © **{currentYear}** {CompanyName}. All rights reserved.
        </p>
    </div>
</body>
</html>
";

            // --- Placeholder Replacement Logic ---
            string mailBody = emailTemplate
                .Replace("[RESET_LINK_PLACEHOLDER]", passwordResetLink);
            // Note: The other placeholders are now replaced using C# string interpolation directly in the template.
            // -------------------------------------

            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_informationVM.Email); 
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = $"Password Reset Request for {CompanyName}";
            mailMessage.Body = mailBody; // Use the processed body
            mailMessage.IsBodyHtml = true;

            await stmpClient.SendMailAsync(mailMessage);
        }
    }
}