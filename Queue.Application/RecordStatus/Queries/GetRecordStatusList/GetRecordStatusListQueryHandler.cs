using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.RecordStatus.Queries.GetRecordStatusList;

public class GetRecordStatusListQueryHandler(IRecordStatusRepository recordStatusRepository, ILogger<GetRecordStatusListQueryHandler> logger) :IRequestHandler<GetRecordStatusListQuery,Result<List<Domain.Entites.RecordStatus>>>
{
    public async Task<Result<List<Domain.Entites.RecordStatus>>> Handle(GetRecordStatusListQuery request,CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение полного списка статусов записей из базы данных.");
        var result = await recordStatusRepository.GetAllAsync();
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение полного списка статусов записей из базы данных.", result.Error.Code);
            return Result.Failure<List<Domain.Entites.RecordStatus>>(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return result;
    }
}

