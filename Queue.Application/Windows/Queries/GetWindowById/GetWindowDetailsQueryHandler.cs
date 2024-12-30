using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Interfaces;

namespace Queue.Application.Windows.Queries.GetWindowDetails;

public class GetWindowDetailsQueryHandler(IWindowRepository windowRepository, ILogger<GetWindowDetailsQueryHandler> logger) :IRequestHandler<GetWindowDetailsQuery,Result>
{
    public async Task<Result> Handle(GetWindowDetailsQuery request,CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение окна из базы данных.");
        var result=await windowRepository.GetWindowById(request.WindowId);
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение окна из базы данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return result;
    }
}
