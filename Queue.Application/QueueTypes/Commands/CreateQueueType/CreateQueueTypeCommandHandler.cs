using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.QueueTypes.Commands.CreateQueueType;

public class CreateQueueTypeCommandHandler(IQueueTypeRepository queueTypeRepository,ILogger<CreateQueueTypeCommandHandler> _logger) : IRequestHandler<CreateQueueTypeCommand, Result>
{
    private static readonly TimeSpan UtcOffset = TimeSpan.FromHours(5);
    public async Task<Result> Handle(CreateQueueTypeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Обработка запроса на создание нового типа очереди в базе данных.");
        var queueType = new QueueType
        {
            NameRu=request.NameRu,
            NameKk=request.NameKk,
            NameEn=request.NameEn,
            DescriptionRu=request.DescriptionRu,
            DescriptionKk=request.DescriptionKk,
            DescriptionEn=request.DescriptionEn,
            CreatedOn = DateTimeOffset.UtcNow.ToOffset(UtcOffset).DateTime,
            CreatedBy = request.CreatedBy,
        };
        var result=await queueTypeRepository.AddAsync(queueType);
        if (result.IsFailed)
        {
            _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на создание нового типа очереди в базе данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        _logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}
