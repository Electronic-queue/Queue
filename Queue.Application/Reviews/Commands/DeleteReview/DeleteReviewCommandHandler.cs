using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;
using Serilog.Core;

namespace Queue.Application.Reviews.Commands.DeleteReview;

public class DeleteReviewCommandHandler(IReviewRepository reviewRepository,ILogger<DeleteReviewCommandHandler> _logger) : IRequestHandler<DeleteReviewCommand, Result>
{
    public async Task<Result> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
         _logger.LogInformation("Обработка запроса на обновление отзыва в базе данных.");

        var result =await reviewRepository.DeleteAsync(request.ReviewId);
        if(result.IsFailed) 
        {

            _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на обновление отзыва в базе данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        _logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}
