using MailSender.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace MailSender.Service
{
    public class DebugMailService : IMailService
    {
        private readonly IEncryptorService _encryptorService;
        public DebugMailService(IEncryptorService encryptorService)
        {
            _encryptorService = encryptorService;
        }
        public IMailSender GetSender(string server, int port, bool ssl, string login, string password)
        { 
            return new DebugMailSender(server, port, ssl, login, password);
        }
        private class DebugMailSender : IMailSender
        {
            private readonly string _address;
            private readonly int _port;
            private readonly bool _ssl;
            private readonly string _login;
            private readonly string _password;
            public DebugMailSender(string address, int port, bool ssl, string login, string password)
            {
                _address = address;
                _port = port;
                _ssl = ssl;
                _login = login;
                _password = password;
            }

            public void Send(string senderAddress, string recipientAddress, string subject, string body)
            {
                Debug.WriteLine
                    ($"Отправка почты через сервер {_address}:{_port} SSL:{_ssl} (Login: {_login} Pass: {_password})");
                Debug.WriteLine
                    ($"Сообщение от {senderAddress} к {recipientAddress}:\r\n{subject}\r\n{body}");
            }
            public void Send(string senderAddress, IEnumerable<string> recipientAddresses, string subject, string body)
            {
                foreach (var recipientAddress in recipientAddresses)
                {
                    Send(senderAddress, recipientAddress, subject, body);
                }
            }

            public Task SendAsync(string senderAddress, string recipientAddress, string subject, string body, CancellationToken cancel = default)
            {
                throw new System.NotImplementedException();
            }

            public Task SendAsync(string senderAddress, IEnumerable<string> recipientAddresses, string subject, string body, System.IProgress<(string recipient, double percent)> progress = null, CancellationToken cancel = default)
            {
                throw new System.NotImplementedException();
            }

            public void SendParallel(string senderAddress, IEnumerable<string> recipientAddresses, string subject, string body)
            {
                Send(senderAddress, recipientAddresses, subject, body);
            }

            public Task SendParallelAsync(string senderAddress, IEnumerable<string> recipientAddresses, string subject, string body, CancellationToken cancel = default)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}