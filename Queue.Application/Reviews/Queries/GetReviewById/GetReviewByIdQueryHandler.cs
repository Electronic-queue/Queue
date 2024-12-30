using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Interfaces;

namespace Queue.Application.Reviews.Queries.GetReviewById;

public class GetReviewByIdQueryHandler(IReviewRepository reviewRepository, ILogger<GetReviewByIdQueryHandler> logger) : IRequestHandler<GetReviewByIdQuery, Result>
{
    public async Task<Result> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение отзыва из базы данных.");
        var result = await reviewRepository.GetReviewdById(request.ReviewId);
        if(result.IsFailed) {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение отзыва из базы данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}
