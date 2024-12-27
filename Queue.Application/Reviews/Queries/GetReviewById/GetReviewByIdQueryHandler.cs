using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Interfaces;

namespace Queue.Application.Reviews.Queries.GetReviewById;

public class GetReviewByIdQueryHandler(IReviewRepository reviewRepository) : IRequestHandler<GetReviewByIdQuery, Result>
{
    public async Task<Result> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await reviewRepository.GetReviewdById(request.ReviewId);
        if(result.IsFailed) {
            return Result.Failure(new Error("405", "Error"));
        }
        return Result.Success();
    }
}
