using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Interfaces;

namespace Queue.Application.RecordStatus.Commands.DeleteRecordStatus;

public class DeleteRecordStatusCommandHandler(IRecordStatusRepository recordStatusRepository,ILogger<DeleteRecordStatusCommandHandler> _logger):IRequestHandler<DeleteRecordStatusCommand,Result>
{
    public async Task<Result> Handle(DeleteRecordStatusCommand request,CancellationToken cancellationToken)
    {
        _logger.LogInformation("Обработка запроса на удаление статуса записи из базы данных.");
        var result = await recordStatusRepository.DeleteAsync(request.RecordStatusId);
        if (result.IsFailed)
        {
            _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на удаление статуса записи из базы данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        _logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();

    }
}
