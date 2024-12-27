using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;

namespace Queue.Application.WindowStatuses.Commands.UpdateWIndowStatus;

public class UpdateWindowStatusCommandHandler(IWindowStatusRepository windowStatusRepository,ILogger<UpdateWindowStatusCommandHandler> _logger) : IRequestHandler<UpdateWindowStatusCommand, Result>
{
    public async Task<Result> Handle(UpdateWindowStatusCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос на обновление статуса окна");
        var windowStatus = await windowStatusRepository.UpdateAsync(
            windowStatusId: request.WindowStatusId,
            nameRu: request.NameRu,
            nameKk: request.NameKk,
            nameEn: request.NameEn,
            descriptionRu: request.DescriptionRu,
            descriptionKk: request.DescriptionKk,
            descriptionEn: request.DescriptionEn
            );
        if (windowStatus.IsFailed)
        {
            _logger.LogError($"Ошибка при обновлении статуса окна с id: {request.WindowStatusId}.");
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка обновления статуса окна"));
        }
        _logger.LogInformation($"Успешное обновление статуса окна с id: {request.WindowStatusId}.");
        return Result.Success();
    }
}
