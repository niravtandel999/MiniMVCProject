using Microsoft.Extensions.Options;
using MimeKit;
using MVCTaskProject.Configuration;
using MVCTaskProject.Models;
using MailKit.Net.Smtp;


namespace MVCTaskProject.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;

        public MailService(IOptions<MailSettings> options)
        {
            _mailSettings = options.Value;
        }

        public bool SendEmail(SendEmail email)
        {
            try
            {
                Console.WriteLine("Preparing to send email...");

                using (var emailMessage = new MimeMessage())
                {
                    var from = new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail);
                    emailMessage.From.Add(from);

                    var to = new MailboxAddress(null, email.Email);
                    emailMessage.To.Add(to);

                    emailMessage.Subject = email.Subject;

                    var bodyBuilder = new BodyBuilder
                    {
                        TextBody = email.Body
                    };
                    emailMessage.Body = bodyBuilder.ToMessageBody();

                    using (var mailClient = new SmtpClient())
                    {
                        Console.WriteLine("Connecting to SMTP server...");
                        mailClient.Connect(_mailSettings.Server, _mailSettings.port, MailKit.Security.SecureSocketOptions.StartTls);
                        Console.WriteLine("Authenticating...");
                        mailClient.Authenticate(_mailSettings.SenderEmail, _mailSettings.Password);
                        Console.WriteLine("Sending email...");
                        mailClient.Send(emailMessage);
                        Console.WriteLine("Email sent successfully.");
                        mailClient.Disconnect(true);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }


    }
}
