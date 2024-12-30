using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.UserServices.Queries.GetUserServiceList;

public class GetUserServiceListQueryHandler(IUserServiceRepository userServiceRepository, ILogger<GetUserServiceListQueryHandler> logger) : IRequestHandler<GetUserServiceListQuery,Result<List<UserService>>>
{ 
    public async Task<Result<List<UserService>>> Handle(GetUserServiceListQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение полного списка действий из базы данных.");
        var result = await userServiceRepository.GetAllAsync();

        if (result.IsFailed)
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение полного списка действий из базы данных.", result.Error.Code);
            return Result.Failure<List<UserService>>(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return result;
    }
}
