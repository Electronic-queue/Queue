using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.Reviews.Commands.DeleteReview;

public record DeleteReviewCommand(
    int ReviewId
    ):IRequest<Result>;
