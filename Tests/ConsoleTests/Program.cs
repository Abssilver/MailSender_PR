using MailSender.Interfaces;
using MailSender.Service;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            //ThreadTests.Start();
            CriticalSectionTest.Start();

            Console.WriteLine("Главный поток работу закончил");
            Console.ReadLine();
        } 
    }
}
