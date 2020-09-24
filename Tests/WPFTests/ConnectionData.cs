namespace WPFTests
{
    static class ConnectionData
    {
        public static string ClientEmail { get; } = "test@gmail.com";
        public static string AddresseeEmail { get; } = "test@gmail.com";
        public static string SmtpServer { get; } = "smtp.yandex.ru";
        public static int SmtpPort { get; } = 587;
        public static string AddresseeUsername { get; } = "Addressee";
        public static string ClientUsername { get; } = "Client";
    }
}
