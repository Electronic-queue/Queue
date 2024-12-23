using KDS.Primitives.FluentResult;
using Queue.Domain.Entites;

namespace Queue.Domain.Interfaces;

public interface INotificationRepository : IRepository<Notification>
{
    Task<Result<Notification>> GetNotificationById(int id);
}
