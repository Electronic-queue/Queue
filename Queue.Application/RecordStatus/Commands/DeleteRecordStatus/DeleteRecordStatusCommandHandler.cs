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

namespace Queue.Application.RecordStatus.Commands.DeleteRecordStatus
{
    public class DeleteRecordStatusCommandHandler(IRecordStatusRepository recordStatusRepository,ILogger<DeleteRecordStatusCommandHandler> _logger):IRequestHandler<DeleteRecordStatusCommand,Result>
    {
        public async Task<Result> Handle(DeleteRecordStatusCommand request,CancellationToken cancellationToken)
        {
            _logger.LogInformation("Запрос на удалениие статуса записи");
            var recordStatus = await recordStatusRepository.DeleteAsync(request.RecordStatusId);
            if (recordStatus.IsFailed)
            {
                _logger.LogError($"Ошибка при удалении статуса записи с id: {request.RecordStatusId}.");
                return Result.Failure(new Error(Errors.BadRequest, "Ошибка удаления статуса записи"));
            }
            _logger.LogInformation($"Успешное удаление статуса записи с id: {request.RecordStatusId}.");
            return Result.Success();

        }
    }
}
