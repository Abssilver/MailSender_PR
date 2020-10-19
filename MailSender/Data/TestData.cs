using MailSender.lib.Service;
using MailSender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MailSender.Data
{
    static class TestData
    {
        public static List<Sender> Senders { get; } = Enumerable.Range(1, 10)
            .Select(i => new Sender
            {
                Id = i,
                Name = $"Отправитель {i}",
                Address = $"sender_{i}@server.ru",
                Description = $"Почта от Отправитель {i}"
            }).ToList();
        public static List<Recipient> Recipients { get; } = Enumerable.Range(1, 10)
            .Select(i => new Recipient
            {
                Id = i,
                Name = $"Получатель {i}",
                Address = $"recipient_{i}@server.ru",
                Description = $"Почта для Получатель {i}"
            }).ToList();
        public static List<Server> Servers { get; } = new List<Server>
        {
            new Server
            {
                Id = 1,
                Name = "Яндекс",
                Address = "smpt.yandex.ru",
                Port = 587,
                UseSSL = true,
                Login = "user@yandex.ru",
                Password = TextEncoder.Encode($"Password-Яндекс")
            },
            new Server
            {
                Id = 2,
                Name = "Google",
                Address = "smtp.gmail.com",
                Port = 58,
                UseSSL = true,
                Login = "user@gmail.com",
                Password = TextEncoder.Encode($"Password-Google")
            },
            new Server
            {
                Id = 3,
                Name = "MailRu",
                Address = "smtp.mail.ru",
                Port = 25,
                UseSSL = true,
                Login = "user@mail.ru",
                Password = TextEncoder.Encode($"Password-MailRu")
            }
        };
        public static List<Message> Messages { get; } = Enumerable.Range(1, 20)
           .Select(i => new Message
           {
               Id = i,
               Subject = $"Сообщение {i}",
               Body = $"Текст сообщения {i}"
           }).ToList();
    }
}
