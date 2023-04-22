using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using MTGDeckBuilder.DTO.Email;

namespace MTGDeckBuilder.Services
{
    public class EmailService : IEmailService
    {
        public EmailService()
        {

        }

        public void SendEmail(EmailDto request, IConfiguration configuration)
        {
            var email= new MimeMessage();
            email.From.Add(MailboxAddress.Parse(configuration["Google:SmtpUsername"]));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(configuration["Google:SmtpUsername"], configuration["Google:SmtpPassword"]);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
