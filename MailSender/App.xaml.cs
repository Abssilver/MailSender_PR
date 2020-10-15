using MailSender.Interfaces;
using MailSender.Service;
using MailSender.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace MailSender    
{
    public partial class App
    {
        private static IHost _hosting;
        public static IHost Hosting => _hosting ??= Host.CreateDefaultBuilder(Environment.GetCommandLineArgs())
            .ConfigureAppConfiguration(cfg => cfg
            .AddJsonFile("appsettings.json", true, true)
            .AddXmlFile("appsettings.xml", true, true))
            .ConfigureLogging(log => log
            .AddConsole()
            .AddDebug())
            .ConfigureServices(ConfigureServices)
            .Build();
        public static IServiceProvider Services => Hosting.Services;
        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();
#if DEBUG
            services.AddTransient<IMailService, DebugMailService>();
#else
            services.AddTransient<IMailService, SmtpMailService>();
#endif
            services.AddSingleton<IEncryptorService, Rfc2898Encryptor>();
            //services.AddScoped<>();
        }
    }
}
