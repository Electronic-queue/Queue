using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;
using Serilog.Core;

namespace Queue.Application.ExceedingsTimes.Commands.UpdateExceedingsTime;

public class UpdateExceedingsTimeCommandHandler(IExceedingsTimeRepository timeRepository,ILogger<UpdateExceedingsTimeCommandHandler> _logger) : IRequestHandler<UpdateExceedingsTimeCommand, Result>
{
    public async Task<Result> Handle(UpdateExceedingsTimeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос на обновление времени перерыва");
        var result = await timeRepository.UpdateAsync(
            exceedingsTimeId:request.ExceedingsTimeId,
            windowId:request.WindowId,
            timeForExcommunication:request.TimeForExcommunication,
            canceledOn:request.CanceledOn
            );
        if (result.IsFailed)
        {
            _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на обновление времени перерыва в базе данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        _logger.LogInformation($"Успешное обновление времени отдыха с id: {request.ExceedingsTimeId}.");
        return Result.Success();
    }
}
