using MailSender.Interfaces;
using System.Diagnostics;

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
        }
    }
}