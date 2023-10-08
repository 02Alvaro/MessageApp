namespace Messaging.Domain
{
    public interface MessageRepository
    {
        Task<Message> GetById(Guid id);
        Task<IEnumerable<Message>> GetAll();
        Task Create(Message message);
        Task Update(Message message);
        Task Delete(Guid id);
    }
}
