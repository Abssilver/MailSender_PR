using MailSender.lib;
using MailSender.Models;
using System.Net.Mail;
using System.Windows;

namespace MailSender
{
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void OnSendButtonClick(object sender, RoutedEventArgs args)
        {
            if (!(SendersList.SelectedItem is Sender selectedSender)) return;
            if (!(RecipientsList.SelectedItem is Recipient selectedRecipient)) return;
            if (!(ServersList.SelectedItem is Server selectedServer)) return;
            if (!(MessagesList.SelectedItem is Message selectedMessage)) return;

            var sendService = new MailSenderService()
            {
                ServerAddress = selectedServer.Address,
                ServerPort = selectedServer.Port,
                UseSSL = selectedServer.UseSSL,
                Login = selectedServer.Login,
                Password = selectedServer.Password
            };
            try
            {
                sendService.SendMessage
                    (selectedSender.Address, selectedRecipient.Address, 
                    selectedMessage.Subject, selectedMessage.Body);
            }
            catch (SmtpException ex)
            {
                MessageBox.Show("Ошибка при отправке почты\n" + ex.ToString(), "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
