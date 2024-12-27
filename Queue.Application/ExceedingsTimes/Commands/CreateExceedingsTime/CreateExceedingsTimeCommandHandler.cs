using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.ExceedingsTimes.Commands.CreateExceedingsTime;

public class CreateExceedingsTimeCommandHandler(IExceedingsTimeRepository timeRepository,ILogger<CreateExceedingsTimeCommandHandler> _logger) : IRequestHandler<CreateExceedingsTimeCommand, Result>
{
    private static readonly TimeSpan UtcOffset = TimeSpan.FromHours(5);
    public async Task<Result> Handle(CreateExceedingsTimeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос на создание времени перерыва");
        var exceedingsTime = new ExceedingsTime
        {

            WindowId= request.WindowId,
            TimeForExcommunication=request.TimeForExcommunication,
            CreatedOn = DateTimeOffset.UtcNow.ToOffset(UtcOffset).DateTime,
            CanceledOn = DateTimeOffset.UtcNow.ToOffset(UtcOffset).DateTime
        };
        var result=await timeRepository.AddAsync(exceedingsTime);
        if (result.IsFailed)
        {
            _logger.LogError($"Ошибка при создании времени перерыва.");
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка создания времени перерыва."));
        }
        _logger.LogInformation($"Успешное создание времени перерыва ");
        return Result.Success();
    }
}
