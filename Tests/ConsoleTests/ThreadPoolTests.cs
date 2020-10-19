using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleTests
{
    static class ThreadPoolTests
    {
        public static void Start()
        {
            var messages = Enumerable.Range(1, 1_000)
                .Select(i => $"Message {i}")
                .ToArray();
            var timer = Stopwatch.StartNew();

            ThreadPool.GetAvailableThreads(out var availableWorkerThreads, out var availableCompletionThreads);
            ThreadPool.GetMinThreads(out var minWorkerThreads, out var minCompletionThreads);
            ThreadPool.GetMaxThreads(out var maxWorkerThreads, out var maxCompletionThreads);

            /*
            ThreadPool.SetMinThreads(4, 4);
            ThreadPool.SetMaxThreads(16, 16);
            */
            for (int i = 0; i < messages.Length; i++)
            {
                /*
                var messageNum = i;
                new Thread(() => ProcessMessage(messages[messageNum]))
                { 
                    IsBackground = true
                }.Start();
                */
                ThreadPool.QueueUserWorkItem(item => ProcessMessage((string)item), messages[i]);
            }
            timer.Stop();
            Console.WriteLine(timer.Elapsed.TotalSeconds);
        }
        private static void ProcessMessage(string message)
        {
            Console.WriteLine($"Обработка сообщения {message}");
            Thread.Sleep(1000);
            Console.WriteLine($"Обработка сообщения {message} завершена");
        }
    }
}
