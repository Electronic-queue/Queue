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

namespace Queue.Application.RecordStatus.Commands.CreateRecordStatus
{
    public class CreateRecordStatusCommandHandler(IRecordStatusRepository recordStatusRepository,ILogger<CreateRecordStatusCommandHandler> _logger):IRequestHandler<CreateRecordStatusCommand,Result>
    {
        private static readonly TimeSpan UtcOffset = TimeSpan.FromHours(5);
        public async Task<Result> Handle(CreateRecordStatusCommand request,CancellationToken cancellationToken)
        {
            _logger.LogInformation("Запрос на создание статуса записи");
            var recordStatus = new Domain.Entites.RecordStatus
            {
                NameRu = request.NameRu,
                NameKk = request.NameKk,
                NameEn = request.NameEn,
                DescriptionRu = request.DescriptionRu,
                DescriptionKk = request.DescriptionKk,
                DescriptionEn = request.DescriptionEn,
                CreatedOn = DateTimeOffset.UtcNow.ToOffset(UtcOffset).DateTime,
                CreatedBy = request.CreatedBy
            };
            var result=await recordStatusRepository.AddAsync(recordStatus);
            if (result is null)
            {
                _logger.LogError($"ошибка при создании статуса записи");
                return Result.Failure<int>(new Error(Errors.BadRequest, "Ошибка добавления статуса записи"));
            }
            _logger.LogInformation($"Успешное создание статуса записи");
            return Result.Success();
        }
    }
}
