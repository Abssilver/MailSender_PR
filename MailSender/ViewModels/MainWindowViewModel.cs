using MailSender.Data;
using MailSender.Infrastructure.Commands;
using MailSender.Models;
using MailSender.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks.Dataflow;
using System.Windows;
using System.Windows.Input;

namespace MailSender.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private string _title = "Тестовое окно";
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

        #endregion
        public MainWindowViewModel()
        {
            Servers = new ObservableCollection<Server>(TestData.Servers);
            Senders = new ObservableCollection<Sender>(TestData.Senders);
            Recipients = new ObservableCollection<Recipient>(TestData.Recipients);
            Messages = new ObservableCollection<Message>(TestData.Messages);

        }
    }
}
