using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.ExceedingsTimes.Queries.GetExceedingsTimeById;

public class GetExceedingsTimeByIdQueryHandler(IExceedingsTimeRepository timeRepository, ILogger<GetExceedingsTimeByIdQueryHandler> logger) : IRequestHandler<GetExceedingsTimeByIdQuery, Result>
{
    public async Task<Result> Handle(GetExceedingsTimeByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение времени перерыва из базы данных.");
        var result = await timeRepository.GetExceedingsTimeById(request.ExceedingsTimeId);
        if(result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение времени перерыва из базы данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }

        logger.LogInformation("Запрос успешно обработан.");
        return result; 
    }
}
