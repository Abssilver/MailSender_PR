using System;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace WPFTests
{
    public partial class WpfMailSender 
    {
        public WpfMailSender()
        {
            InitializeComponent();
            
        }

        private void OnSendButtonClick(object sender, RoutedEventArgs e)
        {
            ConnectionData.ClientEmail = tbSender.Text;
            ConnectionData.AddresseeEmail = tbAddressee.Text;

            EmailSendServiceClass sendService = new EmailSendServiceClass();

            sendService.SetupCredentials(tbLogin.Text, pbPassword.SecurePassword);
            sendService.SetupMessage(EmailSubjectTb.Text, EmailBodyTb.Text);
            try
            {
                sendService.SendMessage();
            }
            catch (SmtpException ex)
            {
                ErrorMessageWindow errorWindow = 
                    new ErrorMessageWindow("Невозможно отправить письмо:\n" + ex.ToString());
                errorWindow.ShowDialog();
            }
            SendEndWindow endWindow = new SendEndWindow();
            endWindow.ShowDialog();
        }
    }
}
