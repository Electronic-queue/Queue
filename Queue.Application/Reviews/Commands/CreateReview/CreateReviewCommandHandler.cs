using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.Reviews.Commands.CreateReview;

public class CreateReviewCommandHandler(IReviewRepository reviewRepository,ILogger<CreateReviewCommandHandler> _logger) : IRequestHandler<CreateReviewCommand, Result>
{
    private static readonly TimeSpan UtcOffset = TimeSpan.FromHours(5);
    public async Task<Result> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Обработка запроса на создание нового отзыва в базе данных.");
        var review = new Review
        {
            RecordId = request.RecordId,
            Rating = request.Rating,
            Content = request.Content,
            CreatedOn = DateTimeOffset.UtcNow.ToOffset(UtcOffset).DateTime
        };
        var result=await reviewRepository.AddAsync(review);
        if (result.IsFailed)
        {
            _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на создание нового отзыва в базе данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        _logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}
