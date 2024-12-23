using KDS.Primitives.FluentResult;
using Queue.Domain.Entites;

namespace Queue.Domain.Interfaces;

public interface INotificationTypeRepository : IRepository<NotificationType>
{
    Task<Result<NotificationType>> GetNotificationTypeById(int id);
}
