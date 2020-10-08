using MailSender.Models;
using System.Diagnostics;
using System.Net.Mail;
using System.Windows;
using System.Linq;
using MailSender.Data;
using MailSender.Service;

namespace MailSender
{
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>Обработчик события кнопки создания нового сервера</summary>
        /*
        private void OnAddServerButtonClick(object Sender, RoutedEventArgs E)
        {
            if (!ServerEditDialog.Create(
            out var name,
            out var address,
            out var port,
            out var ssl,
            out var description,
            out var login,
            out var password))
                return;
            var server = new Server
            {
                Id = TestData.Servers.DefaultIfEmpty().Max(s => s.Id) + 1,
                Name = name,
                Address = address,
                Port = port,
                UseSSL = ssl,
                Description = description,
                Login = login,
                Password = password
            };
            TestData.Servers.Add(server);
            ServersList.ItemsSource = null;
            ServersList.ItemsSource = TestData.Servers;
            ServersList.SelectedItem = server;
        }

        /// <summary>Обработчик события кнопки редактирования сервера</summary>
        private void OnEditServerButtonClick(object Sender, RoutedEventArgs E)
        {
            if (!(ServersList.SelectedItem is Server server)) return;
            var name = server.Name;
            var address = server.Address;
            var port = server.Port;
            var ssl = server.UseSSL;
            var description = server.Description;
            var login = server.Login;
            var password = server.Password;
            if (!ServerEditDialog.ShowDialog("Редактирование сервера",
            ref name,
            ref address, ref port, ref ssl,
            ref description,
            ref login, ref password))
                return;
            server.Name = name;
            server.Address = address;
            server.Port = port;
            server.UseSSL = ssl;
            server.Description = description;
            server.Login = login;
            server.Password = password;
            ServersList.ItemsSource = null;
            ServersList.ItemsSource = TestData.Servers;
        }

        /// <summary>Обработчик события кнопки удаления сервера</summary>
        private void OnDeleteServerButtonClick(object Sender, RoutedEventArgs E)
        {
            if (!(ServersList.SelectedItem is Server server)) return;
            TestData.Servers.Remove(server);
            ServersList.ItemsSource = null;
            ServersList.ItemsSource = TestData.Servers;
            ServersList.SelectedItem = TestData.Servers.FirstOrDefault();
        }
        /// <summary>Обработчик события кнопки немедленной отправки поты</summary>
        private void OnSendButtonClick(object sender, RoutedEventArgs args)
        {
            if (!(SendersList.SelectedItem is Sender selectedSender)) return;
            if (!(RecipientsList.SelectedItem is Recipient selectedRecipient)) return;
            if (!(ServersList.SelectedItem is Server selectedServer)) return;
            if (!(MessagesList.SelectedItem is Message selectedMessage)) return;

            var mailSender = new SmtpSender(
            selectedServer.Address, selectedServer.Port, selectedServer.UseSSL,
            selectedServer.Login, selectedServer.Password);

            try
            {
                var timer = Stopwatch.StartNew();
                mailSender.SendMessage(
                selectedSender.Address, selectedRecipient.Address,
                selectedMessage.Subject, selectedMessage.Body);
                timer.Stop();
                MessageBox.Show(
                $"Почта успешно отправлена за {timer.Elapsed.TotalSeconds:0.##}c",
                "Отправка почты",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
            }
            catch (SmtpException)
            {
                MessageBox.Show(
                "Ошибка при отправке почты",
                "Отправка почты",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            } 
        }
        */
    }
}
