using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.Reviews.Queries.GetReviewList;

public class GetReviewListQueryHandler(IReviewRepository reviewRepository, ILogger<GetReviewListQueryHandler> logger) : IRequestHandler<GetReviewListQuery, Result<List<Review>>>
{
    public async Task<Result<List<Review>>> Handle(GetReviewListQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение полного списка отзывов из базы данных.");
        var result = await reviewRepository.GetAllAsync();
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение полного списка отзывов из базы данных.", result.Error.Code);
            return Result.Failure<List<Review>>(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return м;
    }
}
