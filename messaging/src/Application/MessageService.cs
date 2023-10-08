using Messaging.Domain;

namespace Messaging.Application.Services
{
    public class MessageService
    {
        private readonly MessageRepository _messageRepository;

        public MessageService(MessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<Message> CreateMessage(Sender from, Recipient to, string subject, string body)
        {
            var id = Guid.NewGuid();
            var message = new Message(id, from, to, subject, body);
            await _messageRepository.Create(message);
            return message;
        }

        public async Task<Message> UpdateMessage(Guid id, Sender from, Recipient to, string subject, string body)
        {
            var message = await _messageRepository.GetById(id);
            if (message == null)
            {
                throw new ArgumentException("Message not found", nameof(id));
            }

            message = new Message(id, from, to, subject, body);
            await _messageRepository.Update(message);
            return message;
        }

        public async Task DeleteMessage(Guid id)
        {
            await _messageRepository.Delete(id);
        }

        public async Task<Message> GetMessageById(Guid id)
        {
            return await _messageRepository.GetById(id);
        }

        public async Task<IEnumerable<Message>> GetAllMessages()
        {
            return await _messageRepository.GetAll();
        }
    }
}
