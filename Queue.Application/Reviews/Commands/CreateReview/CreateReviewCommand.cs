using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.Reviews.Commands.CreateReview;

public record CreateReviewCommand(
    int RecordId=0,
    int Rating=0,
    string? Content=null
    
    ):IRequest<Result>;
