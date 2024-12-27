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

namespace Queue.Application.QueueTypes.Commands.DeleteQueueType;

public class DeleteQueueTypeCommandHandler(IQueueTypeRepository queueTypeRepository,ILogger<DeleteQueueTypeCommandHandler> _logger) : IRequestHandler<DeleteQueueTypeCommand, Result>
{
    public async Task<Result> Handle(DeleteQueueTypeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос на удалениие типа очереди");
        var result = await queueTypeRepository.DeleteAsync(request.QueueTypeId);
        if (result.IsFailed)
        {
            _logger.LogError($"Ошибка при удалении типа очереди с id: {request.QueueTypeId}.");
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка удаления типа очереди"));
        }
        _logger.LogInformation($"Успешное удаление уведомления с id: {request.QueueTypeId}.");
        return Result.Success();
    }
}
