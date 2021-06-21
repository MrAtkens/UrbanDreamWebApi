
using System.Threading.Tasks;
using BazarJok.Services.Business.Abstract;
using MailKit.Net.Smtp;
using MimeKit;

namespace BazarJok.Services.Business
{
    public class SmtpEmailSender: IEmailSender
    {
        private readonly string _email;
        private readonly string _password;
        private readonly string _smtpProviderName;
        private readonly int _smtpProviderPort;

        public SmtpEmailSender(string email, string password, string smtpProviderName, int smtpProviderPort)
        {
            _email = email;
            _password = password;
            _smtpProviderName = smtpProviderName;
            _smtpProviderPort = smtpProviderPort;
        }
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="receiver">Email address of a receiver</param>
        /// <param name="subject">Title of Email Message</param>
        /// <param name="body">Main text of the Message</param>
        /// <returns></returns>
        public async Task SendMessage(string receiver, string subject, string body)
        {
            using var client = new SmtpClient();
            
            await client.ConnectAsync(_smtpProviderName, _smtpProviderPort, false);
            await client.AuthenticateAsync(_email, _password);
            
            var emailMessage = new MimeMessage();
 
            emailMessage.From.Add(new MailboxAddress("BazarJok Project", _email));
            emailMessage.To.Add(new MailboxAddress("", receiver));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body
            };
            
            await client.SendAsync(emailMessage);
 
            await client.DisconnectAsync(true);
        }
    }
}