using MailSender.Interfaces;
using MailSender.Service;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Threading;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            //ThreadTests.Start();
            //CriticalSectionTest.Start();
            ThreadPoolTests.Start();

            Console.WriteLine("Главный поток работу закончил");
            Console.ReadLine();
        }

        /*
        [DllImport("filename.dll")]
        private static extern void MethodName(string str);
        */
    }
}
