using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Org.BouncyCastle.Asn1.Pkcs;

namespace MindMission.API.EmailSettings
{
    public class MailService : IMailService
    {
        private readonly MailSettings MailSettings;
        public MailService(IOptions<MailSettings> _MailSettingsOptions)
        {
            MailSettings = _MailSettingsOptions.Value;
        }

        public bool SendMail(MailData MailData)
        {
            try
            {
                /*var message = new MimeMessage();
                //message.From.Add(new MailboxAddress("Sender Name", "tarekeslam33456@gmail.com"));
                //message.To.Add(new MailboxAddress("Recipient Name", "tarekeslam159@gmail.com"));
                //message.Subject = "Hello from MailKit";
                //message.Body = new TextPart("plain")
                //{
                //    Text = "This is the email body."
                //};

                //using (var client = new SmtpClient())
                //{
                //    client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                //    client.Authenticate("mind.mission.site@gmail.com", "tfrcvhowdmewwujc");
                //    client.Send(message);
                //    client.Disconnect(true);
                }*/

                MimeMessage EmailMessage = new MimeMessage();

                EmailMessage.From.Add(new MailboxAddress(MailSettings.SenderName, MailSettings.SenderEmail));
                EmailMessage.To.Add(new MailboxAddress(MailData.EmailToName, MailData.EmailTo));

                EmailMessage.Subject = MailData.EmailSubject;
                BodyBuilder emailBodyBuilder = new BodyBuilder();
                emailBodyBuilder.TextBody = MailData.EmailBody;
                EmailMessage.Body = emailBodyBuilder.ToMessageBody();

                using (SmtpClient MailClient = new SmtpClient())
                {
                    MailClient.Connect(MailSettings.Server, MailSettings.Port, SecureSocketOptions.StartTls);
                    MailClient.Timeout = 6000;
                    MailClient.Authenticate(MailSettings.UserName, MailSettings.Password);
                    MailClient.Send(EmailMessage);
                    MailClient.Disconnect(true);
                }
                Console.WriteLine("Tarek");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
