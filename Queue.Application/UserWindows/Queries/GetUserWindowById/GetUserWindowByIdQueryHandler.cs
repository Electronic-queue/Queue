using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Interfaces;

namespace Queue.Application.UserWindows.Queries.GetUserWindowById;

public class GetUserWindowByIdQueryHandler(IUserWindowRepository userWIndowRepository, ILogger<GetUserWindowByIdQueryHandler> logger) : IRequestHandler<GetUserWindowByIdQuery, Result>
{
    public async Task<Result> Handle(GetUserWindowByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение UserWindow из базы данных.");
        var result = await userWIndowRepository.GetUserWindowById(request.UserWindowId);
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение UserWindow из базы данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return result;
    }
}
