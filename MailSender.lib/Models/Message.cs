using MailSender.Models.Base;

namespace MailSender.Models
{
    public class Message: Entity
    {
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
