using System.Net.Mail;
using System.Text.RegularExpressions;

namespace WPFTests
{
    static class ConnectionData
    {
        private static string yandex = "@yandex.ru$";
        private static string google = "@google.com$";
        private static string mail = "@mail.ru$";
        public static string ClientEmail { get; set; } = "test@yandex.com";
        public static string AddresseeEmail { get; set; } = "test@gmail.com";
        public static string SmtpServer 
        {
            get 
            {
                if (Regex.IsMatch(ClientEmail, yandex)) return SmtpServerYandex;
                else if (Regex.IsMatch(ClientEmail, google)) return SmtpServerGoogle;
                else return SmtpServerMailRu;
            }
        }
        public static int SmtpPort
        {
            get
            {
                if (Regex.IsMatch(ClientEmail, yandex)) return SmtpPortYandex;
                else if (Regex.IsMatch(ClientEmail, google)) return SmtpPortGoogle;
                else return SmtpPortMailRu;
            }
        }
        private static string SmtpServerYandex { get; } = "smtp.yandex.ru";
        private static int SmtpPortYandex { get; } = 587;
        private static string SmtpServerGoogle { get; } = "smtp.gmail.com";
        private static int SmtpPortGoogle { get; } = 58;
        private static string SmtpServerMailRu { get; } = "smtp.mail.ru";
        private static int SmtpPortMailRu{ get; } = 25;
        public static string AddresseeUsername { get; } = "Addressee";
        public static string ClientUsername { get; } = "Client";
        public static bool EnableSSL { get; } = true;
        public static bool IsBodyHtml { get; } = false;
        public static bool UseDefaultCredentials { get; } = false;
        public static SmtpDeliveryMethod DeliveryMethod { get; } = SmtpDeliveryMethod.Network;

    }
}
