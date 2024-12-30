using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.Record.Queries.GetRecodList;

public class GetRecordListQueryHandler(IRecordRepository recordRepository, ILogger<GetRecordListQueryHandler> logger) :IRequestHandler<GetRecordListQuery,Result<List<Domain.Entites.Record>>>
{
    public async Task<Result<List<Domain.Entites.Record>>> Handle(GetRecordListQuery request,CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение полного списка записей из базы данных.");
        var result=await recordRepository.GetAllAsync();
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение полного списка записей из базы данных.", result.Error.Code);
            return Result.Failure<List<Domain.Entites.Record>>(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return result;
    }
}
