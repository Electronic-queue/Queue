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
            _logger.LogInformation("Обработка запроса на создание нового статуса записи в базе данных.");
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
            if (result.IsFailed)
            {
                _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на создание нового статуса записи в базе данных.", result.Error.Code);
                return Result.Failure(result.Error);
            }
            _logger.LogInformation("Запрос успешно обработан.");
            return Result.Success();
        }
    }
}
