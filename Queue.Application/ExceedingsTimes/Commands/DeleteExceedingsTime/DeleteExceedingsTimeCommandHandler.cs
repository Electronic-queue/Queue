using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;

namespace Queue.Application.ExceedingsTimes.Commands.DeleteExceedingsTime;

public class DeleteExceedingsTimeCommandHandler(IExceedingsTimeRepository timeRepository,ILogger<DeleteExceedingsTimeCommandHandler> _logger) : IRequestHandler<DeleteExceedingsTimeCommand, Result>
{
    public async Task<Result> Handle(DeleteExceedingsTimeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос на удалениие времени отдыха");
        var result = await timeRepository.DeleteAsync(request.ExceedingsTimeId);
        if(result.IsFailed)
        {
            _logger.LogError($"Ошибка при удалении времени отдыха с id: {request.ExceedingsTimeId}.");
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка удаления времени отдыха"));
        }
        _logger.LogInformation($"Успешное удаление времени отдыха с id: {request.ExceedingsTimeId}.");
        return Result.Success();
    }
}
