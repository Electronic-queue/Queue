using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;

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
            _logger.LogError($"Ошибка при обновлении времени отдыха с id: {request.ExceedingsTimeId}.");
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка обновления времени отдыха"));
        }
        _logger.LogInformation($"Успешное обновление времени отдыха с id: {request.ExceedingsTimeId}.");
        return Result.Success();
    }
}
