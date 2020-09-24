using System.Net;
using System.Net.Mail;
using System.Windows;

namespace WPFTests
{
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnSendButtonClick(object sender, RoutedEventArgs e)
        {
            var to = new MailAddress(ConnectionData.AddresseeEmail, ConnectionData.AddresseeUsername);
            var from = new MailAddress(ConnectionData.ClientEmail, ConnectionData.ClientUsername);

            var message = new MailMessage(from, to);

            message.Subject = EmailSubjectTb.Text;
            message.Body = EmailBodyTb.Text;

            var client = new SmtpClient(ConnectionData.SmtpServer, ConnectionData.SmtpPort);

            client.EnableSsl = true;

            client.Credentials = new NetworkCredential
            {
                UserName = LoginTb.Text,
                SecurePassword = PasswordPb.SecurePassword
            };

            client.Send(message);
        }
    }
}
