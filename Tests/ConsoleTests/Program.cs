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
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly assembly = Assembly.GetEntryAssembly();

            Type type = assembly.GetType("ConsoleTests.Program");
            Type type2 = assembly.GetTypes().First(type => type.Name.Equals("Program"));

            var str = "test";

            Type type3 = GetObjectType(str);
            var typeString = typeof(string);

            var fileInfo = new FileInfo("TestLib.dll");
            var testLib = Assembly.LoadFile(fileInfo.FullName);
            var printerType = testLib.GetType("TestLib.Printer");
            //constructorInfo
            //MethodInfo
            //ParameterInfo
            //PropertyInfo
            //EventInfo
            //FieldInfo
            foreach (var method in printerType.GetMethods())
            {
                var returnType = method.ReturnType;
                var parameters = method.GetParameters();

                Console.WriteLine
                    ($"{returnType.Name} {method.Name}({string.Join(",", parameters.Select(p=>$"{p.ParameterType.Name} {p.Name}"))})");
            }
            object printer = Activator.CreateInstance(printerType, ">>>");

            var printerConstructor = printerType.GetConstructor(new[] { typeof(string) });
            Console.ReadLine();
        }
        private static Type GetObjectType(object obj)
        {
            return obj.GetType();
        }
    }
}
