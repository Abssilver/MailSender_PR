using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace MailSender.lib
{
    public class MailSenderService
    {
        public string ServerAddress { get; set; }
        public int ServerPort { get; set; }
        public string Login { get; set; }
        public bool UseSSL { get; set; }
        public string Password { get; set; }
        public void SendMessage(string senderAddress, string recipientAddress, string subject, string body)
        {
            var to = new MailAddress(senderAddress); ;
            var from = new MailAddress(recipientAddress);


            using (var message = new MailMessage(from, to))
            {

                message.Subject = subject;
                message.Body = body;


                using (var client = new SmtpClient(ServerAddress, ServerPort))
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
