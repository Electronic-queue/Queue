using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;
using Serilog.Core;

namespace Queue.Application.Record.Commands.UpdateRecord;

public class UpdateRecordCommandHandler(IRecordRepository recordRepository,ILogger<UpdateRecordCommandHandler> _logger):IRequestHandler<UpdateRecordCommand,Result>
{
    public async Task<Result> Handle(UpdateRecordCommand request,CancellationToken cancellationToken)
    {
        _logger.LogInformation("Обработка запроса на обновление записи в базе данных.");
        var result = await recordRepository.UpdateAsync(
                 recordId:request.RecordId,
                 firstName: request.FirstName,
                 lastName: request.LastName,
                 surName: request.Surname,
                 iin: request.Iin,
                 recordStatusId: request.RecordStatusId,
                 serviceId: request.ServiceId,
                 isCreatedByEmployee: request.IsCreatedByEmployee,
                 createdBy: request.CreatedBy,
                 ticketNumber: request.TicketNumber
             );;
       
        if(result.IsFailed)
        {
            _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на обновление записи в базе данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        _logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}
