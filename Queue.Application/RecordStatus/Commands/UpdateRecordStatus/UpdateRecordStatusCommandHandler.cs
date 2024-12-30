using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;

namespace Queue.Application.RecordStatus.Commands.UpdateRecordStatus;

public class UpdateRecordStatusCommandHandler(IRecordStatusRepository recordStatusRepository,ILogger<UpdateRecordStatusCommandHandler> _logger) : IRequestHandler<UpdateRecordStatusCommand, Result>
{
    public async Task<Result> Handle(UpdateRecordStatusCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Обработка запроса на обновление статуса записи в базе данных.");
        var result = await recordStatusRepository.UpdateAsync(
            recordStatusId: request.RecordStatusId,
            nameRu: request.NameRu,
            nameKk: request.NameKk,
            nameEn: request.NameEn,
            descriptionRu: request.DescriptionRu,
            descriptionKk: request.DescriptionKk,
            descriptionEn: request.DescriptionEn,
            createdBy: request.CreatedBy
            ); ;
        if (result.IsFailed)
        {
            _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на обновление статуса записи в базе данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        _logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}
