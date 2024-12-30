using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.Windows.Queries.GetWindowList;

public class GetWindowLisQuerytHandler(IWindowRepository windowRepository, ILogger<GetWindowLisQuerytHandler> logger) :IRequestHandler<GetWindowListQuery,Result<List<Window>>>
{
    public async Task<Result<List<Window>>> Handle(GetWindowListQuery request,CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение полного списка окон из базы данных.");
        var result = await windowRepository.GetAllAsync();
        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение полного списка окон из базы данных.", result.Error.Code);
            return Result.Failure<List<Window>>(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return result;
    }
}
