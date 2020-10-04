
namespace MailSender.Interfaces
{
    public interface IMailService
    {
        IMailSender GetSender(string server, int port, bool ssl, string login, string password);
    }
    public interface IMailSender
    {
        void Send(string senderAddress, string recipientAddress, string subject, string body);
    }
}
