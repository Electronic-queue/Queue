using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.UserWindows.Queries.GetUserWindowList;

public class GetUserWindowQueryHandler(IUserWindowRepository userWIndowRepository, ILogger<GetUserWindowQueryHandler> logger) : IRequestHandler<GetUserWindowListQuery, Result<List<UserWindow>>>
{
    public async Task<Result<List<UserWindow>>> Handle(GetUserWindowListQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение полного списка UserWindow из базы данных.");
        var result = await userWIndowRepository.GetAllAsync();
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение полного списка UserWindow из базы данных.", result.Error.Code);
            return Result.Failure<List<UserWindow>>(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return result;
    }
}
