using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.Reviews.Commands.CreateReview;

public class CreateReviewCommandHandler(IReviewRepository reviewRepository,ILogger<CreateReviewCommandHandler> _logger) : IRequestHandler<CreateReviewCommand, Result>
{
    private static readonly TimeSpan UtcOffset = TimeSpan.FromHours(5);
    public async Task<Result> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос на создание отзыва");
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
            _logger.LogError($"ошибка при создании отзыва");
            return Result.Failure<int>(new Error(Errors.BadRequest, "Ошибка добавления отзыва"));
        }
        _logger.LogInformation($"Успешное создание отзыва");
        return Result.Success();
    }
}
