using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTests
{
    static class AsyncAwaitTest
    {
        private static void PrintThreadInfo(string msg = "")
        {
            var currentThread = Thread.CurrentThread;
            Console.WriteLine($"Thread id: {currentThread.ManagedThreadId}, Task id: {Task.CurrentId}, {msg}");
        }
        public static async Task StartAsync()
        {
            Console.WriteLine("Запуск асинхронного потока");
            PrintThreadInfo("При входе в метод StartAsync");
            
            for (int i = 0; i < 10; i++)
            {
                var resultTask = GetStringResultAsync();
                var result = await resultTask;
                Console.WriteLine($"Получен результат {result}");
            }

            PrintThreadInfo("При выходе из метода StartAsync");
        }
        private static Task<string> GetStringResultAsync()
        {
            PrintThreadInfo("В начале асинхронного метода");
            return Task.Run(() =>
            {
                PrintThreadInfo("Внутри асинхронного метода");
                return DateTime.Now.ToString();
                //return Task.FromResult(DateTime.Now.ToString());
            });
        }

        public static async Task ProcessDataTestAsync()
        {
            Console.WriteLine("Подготовка к обработке сообщений");

            var messages = Enumerable.Range(1, 50).Select(i => $"Messages {i}");
            var tasks = messages.Select(msg => Task.Run(() => LowSpeedPrinter(msg)));
            var runningTasks = tasks.ToArray();

            Console.WriteLine("Задачи созданы");

            await Task.WhenAll(runningTasks);

            Console.WriteLine("Обработка всех сообщений завершена");
        }
        private static void LowSpeedPrinter(string msg)
        {
            Console.WriteLine
                ($"Thread id: {Thread.CurrentThread.ManagedThreadId}, начало обработки сообщения: {msg}");
            Thread.Sleep(100);
            Console.WriteLine
                ($"Thread id: {Thread.CurrentThread.ManagedThreadId}, сообщение обработано: {msg}");
        }
    }
}
