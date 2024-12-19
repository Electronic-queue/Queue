using KDS.Primitives.FluentResult;
using Queue.Domain.Entites;

namespace Queue.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<Result> GetUserDetails(int id);
}
