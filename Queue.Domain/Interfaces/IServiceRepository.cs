using KDS.Primitives.FluentResult;
using Queue.Domain.Entites;

namespace Queue.Domain.Interfaces;

public interface IServiceRepository : IRepository<Service>
{
    Task<Result<Service>> GetServiceById(int id);
}
