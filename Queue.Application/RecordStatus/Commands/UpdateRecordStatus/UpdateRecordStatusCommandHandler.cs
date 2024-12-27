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
        _logger.LogInformation("Запрос на обновление статуса записи");
        var recordStatus = await recordStatusRepository.UpdateAsync(
            recordStatusId: request.RecordStatusId,
            nameRu: request.NameRu,
            nameKk: request.NameKk,
            nameEn: request.NameEn,
            descriptionRu: request.DescriptionRu,
            descriptionKk: request.DescriptionKk,
            descriptionEn: request.DescriptionEn,
            createdBy: request.CreatedBy
            ); ;
        if (recordStatus is null)
        {
            _logger.LogError($"Ошибка при обновлении статуса записи с id: {request.RecordStatusId}.");
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка обновления статуса записи"));
        }
        _logger.LogInformation($"Успешное обновления статуса записи с id: {request.RecordStatusId}.");
        return Result.Success();
    }
}
