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
        _logger.LogInformation("Запрос на обновление UserService");
        var userService = await userServiceRepository.UpdateAsync(
            userServiceId:request.UserServiceId,
            userId:request.UserId,
            serviceId:request.ServiceId,
            descriptionRu:request.DescriptionRu,
            descriptionKk:request.DescriptionKk,
            descriptionEn:request.DescriptionEn,
            isActive:request.IsActive
            );
        if (userService.IsFailed) 
        {
            _logger.LogError($"Ошибка при обновлении userService с id: {request.UserServiceId}.");
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка обновления userService"));
        }
        _logger.LogInformation($"Успешное обновление userService с id: {request.UserServiceId}.");
        return Result.Success();
    }
}
