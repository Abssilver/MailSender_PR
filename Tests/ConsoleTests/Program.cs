using MailSender.Interfaces;
using MailSender.Service;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var task = AsyncAwaitTest.StartAsync();
            var processMessages = AsyncAwaitTest.ProcessDataTestAsync();

            Console.WriteLine("Тестовая задача запущена");
            Task.WaitAll(task, processMessages);
        }
    }
}
