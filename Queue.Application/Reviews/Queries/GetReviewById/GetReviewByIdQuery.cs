using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.Reviews.Queries.GetReviewById;

public record GetReviewByIdQuery(
    int ReviewId
    ):IRequest<Result>;

