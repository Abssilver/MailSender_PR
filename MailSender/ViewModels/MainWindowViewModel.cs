using MailSender.Data;
using MailSender.Infrastructure.Commands;
using MailSender.Interfaces;
using MailSender.Models;
using MailSender.ViewModels.Base;
using Microsoft.Extensions.Configuration;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MailSender.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private readonly IMailService _mailService;
        private readonly IStore<Recipient> _recipientStore;
        private string _title = "Тестовое окно";

        public StatisticsViewModel Statistic { get; } = new StatisticsViewModel();
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        private ObservableCollection<Server> _servers;
        private ObservableCollection<Sender> _senders;
        private ObservableCollection<Recipient> _recipients;
        private ObservableCollection<Message> _messages;

        public ObservableCollection<Server> Servers
        {
            get => _servers;
            set => Set(ref _servers, value);
        }
        public ObservableCollection<Sender> Senders
        {
            get => _senders;
            set => Set(ref _senders, value);
        }
        public ObservableCollection<Recipient> Recipients
        {
            get => _recipients;
            set => Set(ref _recipients, value);
        }
        public ObservableCollection<Message> Messages
        {
            get => _messages;
            set => Set(ref _messages, value);
        }

        private Server _selectedServer;
        public Server SelectedServer
        {
            get => _selectedServer;
            set => Set(ref _selectedServer, value);
        }
        private Sender _selectedSender;
        public Sender SelectedSender
        {
            get => _selectedSender;
            set => Set(ref _selectedSender, value);
        }
        private Recipient _selectedRecipient;
        public Recipient SelectedRecipient
        {
            get => _selectedRecipient;
            set => Set(ref _selectedRecipient, value);
        }
        private Message _selectedMessage;
        public Message SelectedMessage
        {
            get => _selectedMessage;
            set => Set(ref _selectedMessage, value);
        }
        #region Commands

        #region CreateNewServerCommand

        private ICommand _createNewServerCommand;
        public ICommand CreateNewServerCommand => _createNewServerCommand
            ??= new LambdaCommand(OnCreateNewServerCommandExecuted, CanCreateNewServerCommandExecute);
        private bool CanCreateNewServerCommandExecute(object p) => true;

        private void OnCreateNewServerCommandExecuted(object p)
        {
            MessageBox.Show("Создание нового сервера!", "Управление серверами");
        }

        #endregion

        #region EditServerCommand

        private ICommand _editServerCommand;
        public ICommand EditServerCommand => _editServerCommand
            ??= new LambdaCommand(OnEditServerCommandExecuted, CanEditServerCommandExecute);
        private bool CanEditServerCommandExecute(object p) => p is Server || SelectedServer!= null;

        private void OnEditServerCommandExecuted(object p)
        {
            var server = p as Server ?? SelectedServer;
            if (server is null) return;
            MessageBox.Show($"Редактирование сервера {server.Address}!", "Управление серверами");
        }

        #endregion

        #region DeleteServerCommand

        private ICommand _deleteServerCommand;
        public ICommand DeleteServerCommand => _deleteServerCommand
            ??= new LambdaCommand(OnDeleteServerCommandExecuted, CanDeleteServerCommandExecute);
        private bool CanDeleteServerCommandExecute(object p) => p is Server || SelectedServer!= null;

        private void OnDeleteServerCommandExecuted(object p)
        {
            var server = p as Server ?? SelectedServer;
            if (server is null) return;

            Servers.Remove(server);
            SelectedServer = Servers.FirstOrDefault();

            MessageBox.Show($"Удаление сервера {server.Address}!", "Управление серверами");
        }

        #endregion

        #region SendMailCommand

        private ICommand _sendMailCommand;
        public ICommand SendMailCommand => _sendMailCommand
            ??= new LambdaCommand(OnSendMailCommandExecuted, CanSendMailCommandExecute);
        private bool CanSendMailCommandExecute(object p)
        {
            if (SelectedServer is null) return false;
            if (SelectedSender is null) return false;
            if (SelectedRecipient is null) return false;
            if (SelectedMessage is null) return false;
            return true;
        }

        private void OnSendMailCommandExecuted(object p)
        {
            var mailSender = _mailService.GetSender(
                SelectedServer.Address,
                SelectedServer.Port,
                SelectedServer.UseSSL,
                SelectedServer.Login,
                SelectedServer.Password);
            mailSender.Send(
                SelectedSender.Address,
                SelectedRecipient.Address,
                SelectedMessage.Subject,
                SelectedMessage.Body);

            Statistic.MessageSended();
        }

        #endregion
        #endregion
        public MainWindowViewModel(IMailService mailService, IStore<Recipient> recipientStore)
        {
            _mailService = mailService;
            _recipientStore = recipientStore;

            Servers = new ObservableCollection<Server>(TestData.Servers);
            Senders = new ObservableCollection<Sender>(TestData.Senders);
            Recipients = new ObservableCollection<Recipient>(_recipientStore.GetAll());
            Messages = new ObservableCollection<Message>(TestData.Messages);

            //var connection = config.GetConnectionString("Default");
        }
    }
}
