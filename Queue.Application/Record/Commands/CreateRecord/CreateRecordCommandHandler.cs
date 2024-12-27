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

namespace Queue.Application.Record.Commands.CreateRecord
{
    public class CreateRecordCommandHandler(IRecordRepository recordRepository,ILogger<CreateRecordCommandHandler> _logger):IRequestHandler<CreateRecordCommand,Result>
    {
        private static readonly TimeSpan UtcOffset = TimeSpan.FromHours(5);
        public async Task<Result> Handle(CreateRecordCommand request,CancellationToken cancellationToken)
        {
            _logger.LogInformation("Запрос на создание записи");
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
                _logger.LogError($"ошибка при создании записи");
                return Result.Failure<int>(new Error(Errors.BadRequest, "Ошибка добавления записи"));
            }
            _logger.LogInformation($"Успешное создание записи");
            return Result.Success();
        }
    }
}
