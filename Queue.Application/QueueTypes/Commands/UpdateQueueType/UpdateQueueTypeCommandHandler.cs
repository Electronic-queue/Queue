using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Application.QueueTypes.Commands.DeleteQueueType;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.QueueTypes.Commands.UpdateQueueType;

public class UpdateQueueTypeCommandHandler(IQueueTypeRepository queueTypeRepository,ILogger<UpdateQueueTypeCommandHandler> _logger) : IRequestHandler<UpdateQueueTypeCommand, Result>
{
    public async Task<Result> Handle(UpdateQueueTypeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос на обновление типа очереди");
        var queueType = await queueTypeRepository.UpdateAsync(
            queueTypeId: request.QueueTypeId,
            nameRu: request.NameRu,
            nameKk: request.NameKk,
            nameEn: request.NameEn,
            decriptionRu: request.DescriptionRu,
            decriptionKk: request.DescriptionKk,
            decriptionEn: request.DescriptionEn
            );
        if (queueType.IsFailed)
        {
            _logger.LogError($"Ошибка при обновлении типа очереди с id: {request.QueueTypeId}.");
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка обновления типа очереди"));
        }
        _logger.LogInformation($"Успешное обновление типа очереди с id: {request.QueueTypeId}.");
        return Result.Success();

    }
}
