using KDS.Primitives.FluentResult;
using Queue.Domain.Entites;

namespace Queue.Domain.Interfaces;

public interface IWindowRepository
{
    Task<Result> GetWindowById(int id);
    Task<Result<List<Window>>> GetAllAsync();
    Task<Result> AddAsync(Window window);
    Task<Result> DeleteAsync(int id);
    Task<Result> UpdateAsync(int windowId, int? windowStatusId = null, int? windowNumber = null, int? createdBy = null);
}
