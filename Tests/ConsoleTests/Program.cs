using ConsoleTests.Data;
using ConsoleTests.Data.Entities;
using MailSender.Interfaces;
using MailSender.Reports;
using MailSender.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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
        static async Task Main(string[] args)
        {
            var report = new StatisticReport();

            report.SendedMessagesCount = 1000;
            report.CreatePackage("statistics.docx");
            Console.ReadLine();
        }
    }
}
