using KDS.Primitives.FluentResult;
using Queue.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Domain.Interfaces
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task<Result<Notification>> GetNotificationById(int id);
    }
}
