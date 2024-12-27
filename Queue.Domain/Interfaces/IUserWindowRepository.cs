using KDS.Primitives.FluentResult;
using Queue.Domain.Entites;

namespace Queue.Domain.Interfaces;

public interface IUserWindowRepository
{
    Task<Result<List<UserWindow>>> GetAllAsync();
    Task<Result> AddAsync(UserWindow userWindow);
    Task<Result> DeleteAsync(int id);
    Task<Result> GetUserWindowById(int id);
    Task<Result> UpdateAsync(int userWindowId,int? userId=null,int? windowId=null);
}
