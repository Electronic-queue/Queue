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
        _logger.LogInformation("Обработка запроса на обновление статуса окна в базе данных.");
        var result = await windowStatusRepository.UpdateAsync(
            windowStatusId: request.WindowStatusId,
            nameRu: request.NameRu,
            nameKk: request.NameKk,
            nameEn: request.NameEn,
            descriptionRu: request.DescriptionRu,
            descriptionKk: request.DescriptionKk,
            descriptionEn: request.DescriptionEn
            );
        if (result.IsFailed)
        {
            _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на обновление статуса в базе данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        _logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}
