using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace MailSender.Service
{
    public class SmtpSender
    {
        private string Address { get; }
        private int Port { get; }
        private bool UseSSL { get; }
        private string Login { get; }
        private string Password { get; }
        public SmtpSender(string Address, int Port, bool UseSSL, string Login, string Password)
        {
            this.Address = Address;
            this.Port = Port;
            this.UseSSL = UseSSL;
            this.Login = Login;
            this.Password = Password;
        }
        public void SendMessage(string senderAddress, string recipientAddress, string subject, string body)
        {
            var to = new MailAddress(senderAddress);
            var from = new MailAddress(recipientAddress);


            using (var message = new MailMessage(from, to))
            {

                message.Subject = subject;
                message.Body = body;


                using (var client = new SmtpClient(Address, Port))
                {

                    client.EnableSsl = UseSSL;


                    client.Credentials = new NetworkCredential
                    {
                        UserName = Login,
                        Password = Password
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

    }
}
