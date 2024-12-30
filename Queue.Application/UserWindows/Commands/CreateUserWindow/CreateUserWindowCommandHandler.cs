using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.UserWindows.Commands.CreateUserWindow;

public class CreateUserWindowCommandHandler(IUserWindowRepository userWIndowRepository,ILogger<CreateUserWindowCommandHandler> _logger) : IRequestHandler<CreateUserWindowCommand, Result>
{
    private static readonly TimeSpan UtcOffset = TimeSpan.FromHours(5);
    public async Task<Result> Handle(CreateUserWindowCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Обработка запроса на создание нового UserWindow в базе данных.");
        var userWindow = new UserWindow
        {
            UserId= request.UserId,
            WindowId= request.WindowId,
            StartTime = DateTimeOffset.UtcNow.ToOffset(UtcOffset).DateTime,
            EndTime = DateTimeOffset.UtcNow.ToOffset(UtcOffset).DateTime,
            CreatedOn = DateTimeOffset.UtcNow.ToOffset(UtcOffset).DateTime,
            CreatedBy= request.CreatedBy
        };
        var result=await userWIndowRepository.AddAsync(userWindow);
        if (result.IsFailed)
        {
            _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на создание нового UserWindow в базе данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        _logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}
