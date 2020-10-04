using MailSender.Interfaces;
using MailSender.Service;
using System;
using System.Net;
using System.Net.Mail;


namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            IEncryptorService cryptor = new Rfc2898Encryptor();
            var str = "Hello World!";
            const string password = "MailSender!";
            var cryptedString = cryptor.Encrypt(str, password);

            var decryptedString = cryptor.Decrypt(cryptedString, password);
            /*
            var to = new MailAddress("test@gmail.com", "Test To");
            var from = new MailAddress("test@yandex.ru", "Test From");

            var message = new MailMessage(from, to);

            message.Subject = "Заголовок письма от " + DateTime.Now;
            message.Body = "Текст тестового письма " + DateTime.Now;

            var client = new SmtpClient("smtp.yandex.ru", 25);

            client.EnableSsl = true;

            client.Credentials = new NetworkCredential
            {
                UserName = "user_name",
                Password = "password"
            };

            client.Send(message);

            Console.WriteLine("Hello World!");
            */
        }
    }
}
