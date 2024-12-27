using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;

namespace Queue.Application.Reviews.Commands.UpdateReview;

public class UpdateReviewCommandHandler(IReviewRepository reviewRepository,ILogger<UpdateReviewCommandHandler> _logger) : IRequestHandler<UpdateReviewCommand, Result>
{
    public async Task<Result> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос на обновление отзыва");
        var result = await reviewRepository.UpdateAsync(
            reviewId: request.ReviewId,
            recordId: request.RecorId,
            rating: request.Rating,
            content: request.Content
            );
        if (result.IsFailed)
        {
            _logger.LogError($"Ошибка при обновлении отзыва с id: {request.ReviewId}.");
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка обновления отзыва"));
        }
        _logger.LogInformation($"Успешное обновления отзыва с id: {request.ReviewId}.");
        return Result.Success();
    }
}
