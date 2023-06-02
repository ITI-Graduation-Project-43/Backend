namespace MindMission.API.EmailSettings
{
    public interface IMailService
    {
        bool SendMail(MailData mailData);
    }
}