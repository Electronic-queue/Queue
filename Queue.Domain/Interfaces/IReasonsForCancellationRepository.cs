using KDS.Primitives.FluentResult;
using Queue.Domain.Entites;

namespace Queue.Domain.Interfaces;

public interface IReasonsForCancellationRepository
{
    Task<Result<List<ReasonsForCancellation>>> GetAllAsync();
    Task<Result> AddAsync(ReasonsForCancellation reasonsForCancellation);
    Task<Result> DeleteAsync(int id);
    Task<Result> GetReasonsForCancellationById(int id);
    Task<Result> UpdateAsync(int reasonId, int? recordId = null, string? explanation = null);
}
