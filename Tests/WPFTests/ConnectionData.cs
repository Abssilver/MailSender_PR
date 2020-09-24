using System.Net.Mail;

namespace WPFTests
{
    static class ConnectionData
    {
        public static string ClientEmail { get; } = "test@yandex.com";
        public static string AddresseeEmail { get; } = "test@gmail.com";
        public static string SmtpServerYandex { get; } = "smtp.yandex.ru";
        public static int SmtpPortYandex { get; } = 587;
        public static string SmtpServerGoogle { get; } = "smtp.gmail.com";
        public static int SmtpPortGoogle { get; } = 58;
        public static string SmtpServerMailRu { get; } = "smtp.mail.ru";
        public static int SmtpPortMailRu{ get; } = 25;
        public static string AddresseeUsername { get; } = "Addressee";
        public static string ClientUsername { get; } = "Client";
        public static bool EnableSSL { get; } = true;
        public static bool IsBodyHtml { get; } = false;
        public static bool UseDefaultCredentials { get; } = false;
        public static SmtpDeliveryMethod DeliveryMethod { get; } = SmtpDeliveryMethod.Network;

    }
}
