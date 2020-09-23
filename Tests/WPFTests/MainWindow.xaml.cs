using System;
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
            var to = new MailAddress("test@gmail.com", "Test To");
            var from = new MailAddress("test@yandex.ru", "Test From");

            var message = new MailMessage(from, to);

            message.Subject = "Заголовок письма от " + DateTime.Now;
            message.Body = "Текст тестового письма " + DateTime.Now;

            var client = new SmtpClient("smtp.yandex.ru", 587);

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
