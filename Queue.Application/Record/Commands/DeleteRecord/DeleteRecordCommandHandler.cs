using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Record.Commands.DeleteRecord;

public class DeleteRecordCommandHandler(IRecordRepository recordRepository,ILogger<DeleteRecordCommandHandler> _logger):IRequestHandler<DeleteRecordCommand,Result>
{
    public async Task<Result> Handle(DeleteRecordCommand request,CancellationToken cancellationToken)
    {
        _logger.LogInformation("Обработка запроса на удаление записи из базы данных.");
        var result = await recordRepository.DeleteAsync(request.RecordId);
        if (result.IsFailed)
        {
            _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на удаление хаписи из базы данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        _logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}
