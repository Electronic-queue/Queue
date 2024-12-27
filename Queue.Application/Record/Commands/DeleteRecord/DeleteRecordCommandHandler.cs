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
        _logger.LogInformation("Запрос на удалениие записис");
        var record=await recordRepository.DeleteAsync(request.RecordId);
        if (record is null)
        {
            _logger.LogError($"Ошибка при удалении записи с id: {request.RecordId}.");
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка удаления записи"));
        }
        _logger.LogInformation($"Успешное удаление записи с id: {request.RecordId}.");
        return Result.Success();
    }
}
