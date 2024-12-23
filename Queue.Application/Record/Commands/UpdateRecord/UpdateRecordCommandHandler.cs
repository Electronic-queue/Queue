using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;

namespace Queue.Application.Record.Commands.UpdateRecord;

public class UpdateRecordCommandHandler(IRecordRepository recordRepository):IRequestHandler<UpdateRecordCommand,Result>
{
    public async Task<Result> Handle(UpdateRecordCommand request,CancellationToken cancellationToken)
    {
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
            return Result.Failure(new Error(Errors.BadRequest, "UpdateError"));
        }
        return Result.Success();
    }
}
