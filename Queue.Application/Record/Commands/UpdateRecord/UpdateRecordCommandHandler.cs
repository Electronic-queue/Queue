using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;

namespace Queue.Application.Record.Commands.UpdateRecord;

public class UpdateRecordCommandHandler(IRecordRepository recordRepository,ILogger<UpdateRecordCommandHandler> _logger):IRequestHandler<UpdateRecordCommand,Result>
{
    public async Task<Result> Handle(UpdateRecordCommand request,CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос на обновление записи");
        var record = await recordRepository.UpdateAsync(
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
       
        if(record is null)
        {
            _logger.LogError($"Ошибка при обновлении записи с id: {request.RecordId}.");
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка обновления записи"));
        }
        _logger.LogInformation($"Успешное обновления записи с id: {request.RecordId}.");
        return Result.Success();
    }
}
