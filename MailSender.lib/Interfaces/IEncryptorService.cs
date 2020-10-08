namespace MailSender.Interfaces
{
    public interface IEncryptorService
    {
        string Encrypt(string str, string password);
        string Decrypt(string str, string password);
        byte[] Encrypt(byte[] data, string password);
        byte[] Decrypt(byte[] data, string password);
    }
}
