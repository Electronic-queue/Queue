using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;

namespace Queue.Application.ReasonsForCancellations.Queries.ReasonsForCancellationById;

public class GetReasonsForCancellationByIdQueryHandler(IReasonsForCancellationRepository reasonRepository, ILogger<GetReasonsForCancellationByIdQueryHandler> logger) : IRequestHandler<GetReasonsForCancellationByIdQuery, Result>
{
    public async Task<Result> Handle(GetReasonsForCancellationByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение причины для отмены из базы данных.");   
        var result = await reasonRepository.GetReasonsForCancellationById(request.ReasonId);
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение причины для отмены из базы данных.", result.Error.Code);
            return Result.Failure<Action>(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return result;
    }
}
