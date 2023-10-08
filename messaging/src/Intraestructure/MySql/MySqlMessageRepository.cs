using Messaging.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messaging.Intraestructure.MySql
{
    public class MySqlMessageRepository : MessageRepository
    {
        private readonly MessageAppDbContext _dbContext;

        private MessageDTO ToDTO(Message message)
        {
            return new MessageDTO
            {
                Id = message.Id,
                SenderId = Guid.Parse(message.From.Address),
                Subject = message.Subject,
                Body = message.Body
            };
        }

        private async Task<Message> ToDomain(MessageDTO messageDTO)
        {
            var senderDto = await _dbContext.EmailAddresses.FindAsync(messageDTO.SenderId);
            var sender = new Email(senderDto.EmailAddress);

            var recipients = await _dbContext.EmailRecipients
                                             .Where(er => er.MessageId == messageDTO.Id)
                                             .Select(er => er.RecipientId)
                                             .ToListAsync();
            var recipientDto = await _dbContext.EmailAddresses.FindAsync(recipients.FirstOrDefault());
            var recipient = new Email(recipientDto.EmailAddress);

            return new Message(messageDTO.Id, sender, recipient, messageDTO.Subject, messageDTO.Body);
        }

        public MySqlMessageRepository(MessageAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Message> GetById(Guid id)
        {
            var messageDTO = await _dbContext.Messages.FindAsync(id);
            return await ToDomain(messageDTO);
        }

        public async Task<IEnumerable<Message>> GetAll()
        {
            var messagesDTO = await _dbContext.Messages.ToListAsync();
            var messages = new List<Message>();

            foreach (var msgDTO in messagesDTO)
            {
                var message = await ToDomain(msgDTO);
                messages.Add(message);
            }

            return messages;
        }

        public async Task Create(Message message)
        {
            var messageDTO = ToDTO(message);

            var emailRecipient = new EmailRecipientDTO
            {
                MessageId = message.Id,
                RecipientId = Guid.Parse(message.To.Address)
            };
            await _dbContext.EmailRecipients.AddAsync(emailRecipient);

            await _dbContext.Messages.AddAsync(messageDTO);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Message message)
        {
            var messageDTO = ToDTO(message);
            _dbContext.Messages.Update(messageDTO);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var messageDTO = await _dbContext.Messages.FindAsync(id);
            if (messageDTO != null)
            {
                _dbContext.Messages.Remove(messageDTO);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
