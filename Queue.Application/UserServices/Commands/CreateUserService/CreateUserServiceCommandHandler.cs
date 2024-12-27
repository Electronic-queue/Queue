using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.UserServices.Commands.CreateUserService;

public class CreateUserServiceCommandHandler(IUserServiceRepository userServiceRepository,ILogger<CreateUserServiceCommandHandler> _logger) : IRequestHandler<CreateUserServiceCommand, Result>
{
    private static readonly TimeSpan UtcOffset = TimeSpan.FromHours(5);
    public async Task<Result> Handle(CreateUserServiceCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос на создание UserService");
        var userService = new UserService
        {

            UserId = request.UserId,
            ServiceId = request.ServiceId,
            DescriptionRu = request.DescriptionRu,
            DescriptionKk = request.DescriptionKk,
            DescriptionEn = request.DescriptionEn,
            CreatedOn = DateTimeOffset.UtcNow.ToOffset(UtcOffset).DateTime,
            IsActive = true,
            CreatedBy= request.CreatedBy,
        };
        var result=await userServiceRepository.AddAsync(userService);
        if (result.IsFailed)
        {
            _logger.LogError($"Ошибка при создании userService.");
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка создания userService"));
        }
        _logger.LogInformation($"Успешное создание userService");
        return Result.Success();
    }
}
