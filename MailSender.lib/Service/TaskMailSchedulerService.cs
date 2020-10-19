using MailSender.Interfaces;
using MailSender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailSender.Service
{
    public class TaskMailSchedulerService: IMailSchedulerService
    {
        private readonly IStore<SchedulerTask> _tasksStore;
        private readonly IMailService _mailService;
        private Task _schedulerTask;
        private readonly CancellationTokenSource _taskCancellation = new CancellationTokenSource();

        public TaskMailSchedulerService(IStore<SchedulerTask> tasksStore, IMailService mailService)
        {
            this._tasksStore = tasksStore;
            this._mailService = mailService;
        }

        public void Start()
        {
            //var schedulerTask = Task.Run(SchedulerTaskMethod);
            _schedulerTask = Task.Factory.StartNew(SchedulerTaskMethodAsync, TaskCreationOptions.LongRunning);
        }

        public void Stop()
        {
            _taskCancellation.Cancel();
        }
        private async Task SchedulerTaskMethodAsync()
        {
            var cancel = _taskCancellation.Token;
            while (true)
            {
                cancel.ThrowIfCancellationRequested();
                var nextTask = _tasksStore.GetAll()
                    .OrderBy(t => t.Time)
                    .FirstOrDefault(t => t.Time > DateTime.Now);

                if (nextTask is null)
                {
                    break;
                }

                var delay = nextTask.Time - DateTime.Now;
                if (delay.TotalMilliseconds > 0)
                {
                    await Task.Delay(delay, cancel);
                }
                cancel.ThrowIfCancellationRequested();
                await Execute(nextTask);
            }
        }
        private async Task Execute(SchedulerTask task)
        {
            var sender = _mailService
                .GetSender(task.Server.Address, task.Server.Port, task.Server.UseSSL, task.Server.Login, task.Server.Password);
            await sender
                .SendAsync(task.Sender.Address, task.Recipients.Select(recipient => recipient.Address), task.Message.Subject, task.Message.Body);
        }

        public void AddTask(Sender sender, IEnumerable<Recipient> recipients, Server server, Message message, DateTime time)
        {
            Stop();

            _tasksStore.Add(new SchedulerTask
            {
                Sender = sender,
                Recipients = recipients.ToArray(),
                Server = server,
                Message = message,
                Time = time
            });

            Start();
        }
    }
}
