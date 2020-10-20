using MailSender.Data;
using MailSender.Data.Stores.InDB;
using MailSender.Data.Stores.InMemory;
using MailSender.Interfaces;
using MailSender.Models;
using MailSender.Service;
using MailSender.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Windows;

namespace MailSender    
{
    public partial class App
    {
        private static IHost _hosting;
        public static IHost Hosting => _hosting ??= Host.CreateDefaultBuilder(Environment.GetCommandLineArgs())
            .ConfigureHostConfiguration(cfg => cfg
            .AddJsonFile("appconfig.json", true, true)
            .AddXmlFile("appsettings.xml", true, true))
            .ConfigureAppConfiguration(cfg => cfg
            .AddJsonFile("appconfig.json", true, true)
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

            services.AddDbContext<MailSenderDB>(options => options.UseSqlServer(host.Configuration.GetConnectionString("Default")));

            services.AddTransient<MailSenderDbInitializer>();

            //services.AddSingleton<IStore<Recipient>, RecipientsStoreInMemory>();

            services.AddSingleton<IStore<Recipient>, RecipientsStoreInDB>();
            services.AddSingleton<IStore<Sender>, SendersStoreInDB>();
            services.AddSingleton<IStore<Server>, ServersStoreInDB>();
            services.AddSingleton<IStore<Message>, MessagesStoreInDB>();
            services.AddSingleton<IStore<SchedulerTask>, SchedulerTasksStoreInDB>();

            services.AddSingleton<IMailSchedulerService, TaskMailSchedulerService>();

            //services.AddScoped<>();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            Services.GetRequiredService<MailSenderDbInitializer>().Initialize();
            base.OnStartup(e);
            /*
            using (var db = Services.GetRequiredService<MailSenderDB>())
            {
                var toRemove = db.SchedulerTasks.Where(task => task.Time < DateTime.Now);
                if (toRemove.Any())
                {
                    db.SchedulerTasks.RemoveRange(toRemove);
                    db.SaveChanges();
                }
            }
            */
        }
    }
}
