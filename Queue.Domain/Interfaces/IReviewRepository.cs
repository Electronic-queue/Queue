using KDS.Primitives.FluentResult;
using Queue.Domain.Entites;

namespace Queue.Domain.Interfaces;

public interface IReviewRepository
{
    Task<Result<List<Review>>> GetAllAsync();
    Task<Result> AddAsync(Review review);
    Task<Result> DeleteAsync(int id);
    Task<Result> GetReviewdById(int id);
    Task<Result> UpdateAsync(int reviewId, int? recordId = null, int? rating = null, string? content = null);
}
