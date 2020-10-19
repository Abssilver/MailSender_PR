using MailSender.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Interfaces
{
    public interface IMailSchedulerService
    {
        void Start();
        void Stop();
        void AddTask(Sender sender, IEnumerable<Recipient> recipients, Server server, Message message, DateTime time);
    }
}
