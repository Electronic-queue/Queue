using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Interfaces;

namespace Queue.Application.QueueTypes.Commands.DeleteQueueType;

public class DeleteQueueTypeCommandHandler(IQueueTypeRepository queueTypeRepository,ILogger<DeleteQueueTypeCommandHandler> _logger) : IRequestHandler<DeleteQueueTypeCommand, Result>
{
    public async Task<Result> Handle(DeleteQueueTypeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Обработка запроса на удаление типа очереди из базы данных.");
        var result = await queueTypeRepository.DeleteAsync(request.QueueTypeId);
        if (result.IsFailed)
        {
            _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на удаление типа очереди из базы данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        _logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}
