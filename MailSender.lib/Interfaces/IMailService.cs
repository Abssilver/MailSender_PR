
using System.Collections.Generic;

namespace MailSender.Interfaces
{
    public interface IMailService
    {
        IMailSender GetSender(string server, int port, bool ssl, string login, string password);
    }
    public interface IMailSender
    {
        void Send(string senderAddress, string recipientAddress, string subject, string body);

        void Send(string senderAddress, IEnumerable<string> recipientAddresses, string subject, string body);

        void SendParallel(string senderAddress, IEnumerable<string> recipientAddresses, string subject, string body);

    }
}
