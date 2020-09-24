using System;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace WPFTests
{
    public partial class WpfMailSender 
    {
        private EmailSendServiceClass _sendService;
        public WpfMailSender()
        {
            InitializeComponent();
            _sendService = new EmailSendServiceClass();
        }

        private void OnSendButtonClick(object sender, RoutedEventArgs e)
        {
            _sendService.SetupCredentials(LoginTb.Text, PasswordPb.SecurePassword);
            _sendService.SetupMessage(EmailSubjectTb.Text, EmailBodyTb.Text);
            try
            {
                _sendService.SendMessage();
            }
            catch (Exception ex)
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
