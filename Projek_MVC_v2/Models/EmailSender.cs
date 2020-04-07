using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;
using MimeKit;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.IO;
using MimeKit.Utils;

namespace Projek_MVC_v2.Models
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
           
            var emailMessage = new MimeMessage();
            var bodyBuilder = new BodyBuilder();

            //var image = bodyBuilder.LinkedResources.Add(@"wwwroot\images\Logo.png");

            //image.ContentId = MimeUtils.GenerateMessageId();

            //var inlineLogo = bodyBuilder.Attachments.Add(@"wwwroot\images\Logo.png");

      
           



            emailMessage.From.Add(new MailboxAddress("DobroWraca", "dobrowraca1997@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;

            //bodyBuilder.HtmlBody = string.Format(message, image.ContentId);


            emailMessage.Body = new TextPart("html") { Text = message };
            //emailMessage.Body = bodyBuilder.ToMessageBody();



            using (var client = new SmtpClient())
            {
                //client.LocalDomain = "some.domain.com";
                //await client.ConnectAsync("smtp.wp.pl", 465, SecureSocketOptions.Auto).ConfigureAwait(false);
               // await client.SendAsync(emailMessage).ConfigureAwait(false);
               // await client.DisconnectAsync(true).ConfigureAwait(false);



                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTlsWhenAvailable);
                client.Authenticate("dobrowraca1997@gmail.com", "poziom1C");
                client.Send(emailMessage);
                client.Disconnect(true);

            }
        }

    }
}

