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
using System.Linq.Expressions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ConsoleTests
{
    class Program
    {
        [Description("MainProgram")]
        static void Main([Required]string[] args)
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

            var printer2 = printerConstructor.Invoke(new object[] { "<<<" } );

            var printMethodInfo = printerType.GetMethod("Print");

            printMethodInfo.Invoke(printer, new object[] { "ActivatorPrinter" });

            var prefixFieldInfo = printerType.GetField("_prefix", BindingFlags.NonPublic | BindingFlags.Instance);

            object prefixValueObject = prefixFieldInfo.GetValue(printer);

            prefixFieldInfo.SetValue(printer, "new");

            /*
            var appDomain = AppDomain.CurrentDomain;
            var testDomain = AppDomain.CreateDomain("TestDomain");
            testDomain.ExecuteAssemblyByName(...);
            AppDomain.Unload(testDomain);
            */
            /*
            var adminProcessInfo = new ProcessStartInfo(Assembly.GetEntryAssembly().Location, "/RegistryWrite")
            {
            };
            Process process = Process.Start(adminProcessInfo);
            */
            dynamic dynamicPrinter = printer;
            dynamicPrinter.Print("DynamicPrinter");
            //делегат
            Action<string> printLambda = str => Console.WriteLine(str);
            //дерево выражений
            Expression <Action<string>> printExpression = str => Console.WriteLine(str);
            Action<string> compiledExpression = printExpression.Compile();

            var strParam = Expression.Parameter(typeof(string), "str");
            var invokeNode = Expression.Call(
                null,
                typeof(Console).GetMethod("WriteLine", new[] { typeof(string) }),
                strParam);

            var resultExpression = Expression.Lambda<Action<string>>(invokeNode, strParam);
            Action<string> secondExample = resultExpression.Compile();

            compiledExpression("first");
            secondExample("second");

            var programType = typeof(Program);
            var description = programType.GetCustomAttribute<DescriptionAttribute>()?.Description;
            var programMain = programType.GetMethod("Main", BindingFlags.NonPublic | BindingFlags.Static);
            var programMainArgs = programMain.GetParameters().FirstOrDefault();
            var isRequired = programMainArgs.GetCustomAttribute<RequiredAttribute>() != null;
            Console.ReadLine();
        }
        private static Type GetObjectType(object obj)
        {
            return obj.GetType();
        }
    }
}
