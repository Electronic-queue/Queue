using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.Reviews.Queries.GetReviewList;

public class GetReviewListQueryHandler(IReviewRepository reviewRepository) : IRequestHandler<GetReviewListQuery, Result<List<Review>>>
{
    public async Task<Result<List<Review>>> Handle(GetReviewListQuery request, CancellationToken cancellationToken)
    {
        var result = await reviewRepository.GetAllAsync();
        var resultVal = result.Value;
        return Result.Success(resultVal);
    }
}
