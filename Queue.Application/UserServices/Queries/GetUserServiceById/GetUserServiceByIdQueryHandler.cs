using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Interfaces;

namespace Queue.Application.UserServices.Queries.GetUserServiceById;

public class GetUserServiceByIdQueryHandler(IUserServiceRepository userServiceRepository, ILogger<GetUserServiceByIdQueryHandler> logger) : IRequestHandler<GetUserServiceByIdQuery, Result>
{
    public async Task<Result> Handle(GetUserServiceByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса на получение услуги из базы данных.");
        var result = await userServiceRepository.GetUserServiceById(request.UserServiceId);
        if (result.IsFailed) 
        {
            logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на получение услуги из базы данных.", result.Error.Code);
            return Result.Failure<Action>(result.Error);
        }
        logger.LogInformation("Запрос успешно обработан.");
        return result;
    }
}
