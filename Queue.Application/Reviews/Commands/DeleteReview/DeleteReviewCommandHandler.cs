using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;

namespace Queue.Application.Reviews.Commands.DeleteReview;

public class DeleteReviewCommandHandler(IReviewRepository reviewRepository,ILogger<DeleteReviewCommandHandler> _logger) : IRequestHandler<DeleteReviewCommand, Result>
{
    public async Task<Result> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос на удалениие отзыва");
        var result=await reviewRepository.DeleteAsync(request.ReviewId);
        if(result.IsFailed) 
        {
            _logger.LogError($"Ошибка при удалении отзыва с id: {request.ReviewId}.");
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка удаления отзыва"));
        }
        _logger.LogInformation($"Успешное удаление отзыва с id: {request.ReviewId}.");
        return Result.Success();
    }
}
