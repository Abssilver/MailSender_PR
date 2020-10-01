using MailSender.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.ViewModels
{
    class StatisticsViewModel : ViewModel
    {
        private int _sendMessagesCount;
        public int SendMessagesCount
        {
            get => _sendMessagesCount;
            private set => Set(ref _sendMessagesCount, value);
        }
        public void MessageSended() => SendMessagesCount++;
    }
}
