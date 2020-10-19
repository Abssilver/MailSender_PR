using MailSender.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace MailSender.Service
{
    public class SmtpMailService : IMailService
    {
        public SmtpMailService()
        {

        }
        public IMailSender GetSender(string server, int port, bool ssl, string login, string password)
        {
            return new SmtpMailSender(server, port, ssl, login, password);
        }
    }
    internal class SmtpMailSender: IMailSender
    {
        private readonly string _address;
        private readonly int _port;
        private readonly bool _ssl;
        private readonly string _login;
        private readonly string _password;
        public SmtpMailSender(string address, int port, bool ssl, string login, string password)
        {
            _address = address;
            _port = port;
            _ssl = ssl;
            _login = login;
            _password = password;
        }

        public void Send(string senderAddress, string recipientAddress, string subject, string body)
        {
            var to = new MailAddress(senderAddress); ;
            var from = new MailAddress(recipientAddress);


            using (var message = new MailMessage(from, to))
            {

                message.Subject = subject;
                message.Body = body;


                using (var client = new SmtpClient(_address, _port))
                {

                    client.EnableSsl = _ssl;


                    client.Credentials = new NetworkCredential
                    {
                        UserName = _login,
                        Password = _password
                    };
                    try
                    {
                        client.Send(message);
                    }
                    catch (SmtpException ex)
                    {
                        Trace.TraceError(ex.ToString());
                        throw;
                    }

                }
            }
        }
        public void Send(string senderAddress, IEnumerable<string> recipientAddresses, string subject, string body)
        {
            foreach (var recipientAddress in recipientAddresses)
            {
                Send(senderAddress, recipientAddress, subject, body);
            }
        }
        public void SendParallel(string senderAddress, IEnumerable<string> recipientAddresses, string subject, string body)
        {
            foreach (var recipientAddress in recipientAddresses)
            {
                ThreadPool.QueueUserWorkItem(item => Send(senderAddress, recipientAddress, subject, body));
            }
        }
    }
}
