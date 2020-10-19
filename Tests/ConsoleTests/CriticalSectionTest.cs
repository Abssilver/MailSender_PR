using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;

namespace ConsoleTests
{
    static class CriticalSectionTest
    {
        private static readonly object _syncRoot = new object();
        public static void Start()
        {
            LockSynchronizationTest();

            var manualResetEvent = new ManualResetEvent(false);
            var autoResetEvent = new AutoResetEvent(false);

            EventWaitHandle starter = manualResetEvent;

            for (int i = 0; i < 10; i++)
            {
                var threadNum = i;
                new Thread(() =>
                {
                    Console.WriteLine($"Поток {threadNum} запущен", threadNum);
                    starter.WaitOne();
                    Console.WriteLine($"Поток {threadNum} завершил свою работу", threadNum);
                    starter.Reset();
                }).Start();
            }
            Console.WriteLine("Все потоки созданы и готовы к работе.");
            Console.ReadLine();
            starter.Set();
            Console.ReadLine();

            /*
            var mutex1 = new Mutex(true, "Тестовый мютекс", out var created1);
            var mutex2 = new Mutex(true, "Тестовый мютекс", out var created2);

            var semaphore = new Semaphore(0, 10);
            semaphore.WaitOne();
            semaphore.Release();
            */
        }
        private static void LockSynchronizationTest()
        {
            var threads = new Thread[10];
            for (int i = 0; i < threads.Length; i++)
            {
                var threadNum = i;
                threads[i] = new Thread(() => PrintData($"Message from thread {threadNum}", 10));
            }
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Start();
            }
        }
        private static void PrintData(string msg, int count)
        {
            for (int i = 0; i < count; i++)
            {
                lock (_syncRoot)
                {
                    Console.Write("Thread id: {0}", Thread.CurrentThread.ManagedThreadId);
                    Console.Write("\t");
                    Console.Write(msg);
                    Console.WriteLine();
                }
            }
            /*
            Monitor.Enter(_syncRoot);
            try
            {
                Console.Write("Thread id: {0}", Thread.CurrentThread.ManagedThreadId);
                Console.Write("\t");
                Console.Write(msg);
                Console.WriteLine();
            }
            finally
            {
                Monitor.Exit(_syncRoot);
            }
            */
        }
    }
}
