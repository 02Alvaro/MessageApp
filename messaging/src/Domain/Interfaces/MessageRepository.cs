using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Messaging.Domain.Entities;

namespace Messaging.Domain.Repositories
{
    public interface IMessageRepository
    {
        Task<Message> GetById(Guid id);
        Task<IEnumerable<Message>> GetAll();
        Task Create(Message message);
        Task Update(Message message);
        Task Delete(Guid id);
    }
}
