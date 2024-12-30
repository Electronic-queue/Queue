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
            _logger.LogInformation("Обработка запроса на создание новой записи в базе данных.");
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
            if(result.IsFailed)
            {
                _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на создание новой записи в базе данных.", result.Error.Code);
                return Result.Failure(result.Error);
            }
            _logger.LogInformation("Запрос успешно обработан.");
            return Result.Success();
        }
    }
}
