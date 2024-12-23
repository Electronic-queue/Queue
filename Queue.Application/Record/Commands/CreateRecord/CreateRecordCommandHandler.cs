using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Record.Commands.CreateRecord
{
    public class CreateRecordCommandHandler(IRecordRepository recordRepository):IRequestHandler<CreateRecordCommand,Result>
    {
        private static readonly TimeSpan UtcOffset = TimeSpan.FromHours(5);
        public async Task<Result> Handle(CreateRecordCommand request,CancellationToken cancellationToken)
        {
            var record = new Domain.Entites.Record
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Surname = request.Surname,
                Iin=request.Iin,
                RecordStatusId=request.RecordStatusId,
                ServiceId=request.ServiceId,
                StartTime= DateTimeOffset.UtcNow.ToOffset(UtcOffset).DateTime,
                EndTime= DateTimeOffset.UtcNow.ToOffset(UtcOffset).DateTime,
                CreatedOn= DateTimeOffset.UtcNow.ToOffset(UtcOffset).DateTime,
                IsCreatedByEmployee= request.IsCreatedByEmployee,
                CreatedBy= request.CreatedBy,
                TicketNumber= request.TicketNumber,
            };
            var result=await recordRepository.AddAsync(record);
            if(result is null)
            {
                return Result.Failure<int>(new Error(Errors.BadRequest, "Ошибка добавления"));
            }
            return Result.Success();
        }
    }
}
