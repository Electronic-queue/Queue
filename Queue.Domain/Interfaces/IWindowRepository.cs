using KDS.Primitives.FluentResult;
using Queue.Domain.Entites;

namespace Queue.Domain.Interfaces;

public interface IWindowRepository : IRepository<Window>
{
    Task<Result> GetWindowById(int id);
}
