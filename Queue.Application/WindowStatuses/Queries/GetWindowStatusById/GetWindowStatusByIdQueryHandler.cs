using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Interfaces;

namespace Queue.Application.WindowStatuses.Queries.GetWindowStatusById;

public  class GetWindowStatusByIdQueryHandler(IWindowStatusRepository windowStatusRepository, ILogger<GetWindowStatusByIdQueryHandler> logger) :IRequestHandler<GetWindowStatusByIdQuery,Result>
{
    public async Task<Result> Handle (GetWindowStatusByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение статуса окна из базы данных.");
        var result = await windowStatusRepository.GetWindowStatusById(request.WindowStatusId);
        if (result.IsFailed) 
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение статуса окна из базы данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return result;
    }
}
