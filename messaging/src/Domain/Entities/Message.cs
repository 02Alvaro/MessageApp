using Messaging.Domain.Interfaces;

namespace Messaging.Domain.Entities
{
    public class Message
    {
        public Guid Id { get; }
        public string Subject { get; }
        public string Body { get; }
        public Sender From { get; }
        public Recipient To { get; }

        public Message(Guid id, Sender from, Recipient to, string subject, string body)
        {
            Id = id;
            From = from;
            To = to;
            Subject = subject;
            Body = body;
        }
    }
}
