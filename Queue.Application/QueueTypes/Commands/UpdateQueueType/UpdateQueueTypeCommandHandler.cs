using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Interfaces;

namespace Queue.Application.QueueTypes.Commands.UpdateQueueType;

public class UpdateQueueTypeCommandHandler(IQueueTypeRepository queueTypeRepository,ILogger<UpdateQueueTypeCommandHandler> _logger) : IRequestHandler<UpdateQueueTypeCommand, Result>
{
    public async Task<Result> Handle(UpdateQueueTypeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Обработка запроса на обновление типа очереди в базе данных.");
        var result = await queueTypeRepository.UpdateAsync(
            queueTypeId: request.QueueTypeId,
            nameRu: request.NameRu,
            nameKk: request.NameKk,
            nameEn: request.NameEn,
            decriptionRu: request.DescriptionRu,
            decriptionKk: request.DescriptionKk,
            decriptionEn: request.DescriptionEn
            );
        if (result.IsFailed)
        {
            _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на обновление типа очереди в базе данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        _logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();

    }
}
