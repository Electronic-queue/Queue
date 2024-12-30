using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.WindowStatuses.Queries.GetWindowStatusList;

public class GetWindowStatusListQueryHandler(IWindowStatusRepository windowStatusRepository, ILogger<GetWindowStatusListQueryHandler> logger) : IRequestHandler<GetWindowStatusListQuery, Result<List<WindowStatus>>>
{
    public async Task<Result<List<WindowStatus>>> Handle(GetWindowStatusListQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение полного списка статусов окон из базы данных.");
        var result = await windowStatusRepository.GetAllAsync();
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение полного списка статусов окон из базы данных.", result.Error.Code);
            return Result.Failure<List<WindowStatus>>(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return result;
    }
}
