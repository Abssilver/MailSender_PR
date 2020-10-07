using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsoleTests
{
    class PrintMessageTask
    {
        private readonly string _message;
        private readonly int _count;
        private Thread _thread;
        public PrintMessageTask(string message, int count)
        {
            _message = message;
            _count = count;
            _thread = new Thread(ThreadMethod) { IsBackground = true };
        }

        public void Start()
        {
            if (_thread?.IsAlive == false)
            {
                _thread?.Start();
            }
        }
        private void ThreadMethod()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            for (int i = 0; i < _count; i++)
            {
                Console.WriteLine($"Работа потока {threadId}: {i}, {_message}");
                Thread.Sleep(10);
            }

            _thread = null;
        }
    }
}
