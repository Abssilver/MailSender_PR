using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsoleTests
{
    static class ThreadTests
    {
        private static volatile bool _timerWork = true;

        public static void Start()
        {
            var mainThread = Thread.CurrentThread;
            var mainThreadId = mainThread.ManagedThreadId;

            mainThread.Name = "главный поток";

            var timerThread = new Thread(TimerMethod);
            timerThread.Name = "поток часов";
            timerThread.IsBackground = true;
            timerThread.Start();

            var printTask = new PrintMessageTask("In Parameter printer", 10);
            printTask.Start();
            /*
            new Thread(() => PrintMessage("In Parameter printer", 10))
            {
                IsBackground = true,
                Name = "Parameter printer"
            }.Start();
            */
            /*
            var printerThread = new Thread(PrintMessage)
            {
                IsBackground = true,
                Name = "Parameter printer"
            };
            printerThread.Start("In Parameter printer");
            */

           

            Console.WriteLine("Останавливаю время...");

            timerThread.Priority = ThreadPriority.BelowNormal;
            _timerWork = false;
            if (!timerThread.Join(100))
            {
                timerThread.Interrupt();
            }
            //var current_process = System.Diagnostics.Process.GetCurrentProcess();
            //Process.Start("calc.exe");
        }
        private static void PrintMessage(string message, int count)
        {
            PrintThreadInfo();

            //var msg = (string)parameter;

            var threadId = Thread.CurrentThread.ManagedThreadId;
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"Работа потока {threadId}: {i}, {message}");
                Thread.Sleep(10);
            }
        }
        private static void TimerMethod()
        {
            PrintThreadInfo();

            while (_timerWork)
            {
                Console.Title = DateTime.Now.ToString("HH:mm:ss.ffff");
                Thread.Sleep(100);
            }
        }

        private static void PrintThreadInfo()
        {
            var thread = Thread.CurrentThread;
            Console.WriteLine($"id:{thread.ManagedThreadId}; name{thread.Name}; priority:{thread.Priority}");
        }
    }
}
