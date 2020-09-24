using System.Net;
using System.Net.Mail;
using System.Security;

namespace WPFTests
{
    class EmailSendServiceClass
    {
        MailMessage _message;
        SmtpClient _smtpClient;
        public EmailSendServiceClass()
        {
            var client = new MailAddress(ConnectionData.ClientEmail, ConnectionData.ClientUsername);
            var addressee = new MailAddress(ConnectionData.AddresseeEmail, ConnectionData.AddresseeUsername);

            _message = new MailMessage(client, addressee);
            _message.IsBodyHtml = ConnectionData.IsBodyHtml;
            
            _smtpClient = new SmtpClient(ConnectionData.SmtpServerYandex, ConnectionData.SmtpPortYandex);
            _smtpClient.EnableSsl = ConnectionData.EnableSSL;
            _smtpClient.DeliveryMethod = ConnectionData.DeliveryMethod;
            _smtpClient.UseDefaultCredentials = ConnectionData.UseDefaultCredentials;
        }
        public void SetupCredentials(string username, SecureString password)
        {
            _smtpClient.Credentials = new NetworkCredential
            {
                UserName = username,
                SecurePassword = password
            };
        }
        public void SetupMessage(string emailSubject, string emailBody)
        {
            _message.Subject = emailSubject;
            _message.Body = emailBody;
        }
        public void SendMessage() => _smtpClient.Send(_message);
    }
}
