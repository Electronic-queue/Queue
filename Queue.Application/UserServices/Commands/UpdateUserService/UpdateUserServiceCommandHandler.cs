using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;

namespace Queue.Application.UserServices.Commands.UpdateUserService;

public class UpdateUserServiceCommandHandler(IUserServiceRepository userServiceRepository,ILogger<UpdateUserServiceCommandHandler> _logger) : IRequestHandler<UpdateUserServiceCommand, Result>
{
    public async Task<Result> Handle(UpdateUserServiceCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Обработка запроса на обновление услуги в базе данных.");
        var result = await userServiceRepository.UpdateAsync(
            userServiceId:request.UserServiceId,
            userId:request.UserId,
            serviceId:request.ServiceId,
            descriptionRu:request.DescriptionRu,
            descriptionKk:request.DescriptionKk,
            descriptionEn:request.DescriptionEn,
            isActive:request.IsActive
            );
        if (result.IsFailed) 
        {
            _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на обновление услуги в базе данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        _logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}
