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
        _logger.LogInformation("Обработка запроса на создание нового UserService в базе данных.");
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
            _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на создание нового UserService в базе данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        _logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}
