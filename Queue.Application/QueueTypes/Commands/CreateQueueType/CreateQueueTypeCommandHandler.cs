using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.QueueTypes.Commands.CreateQueueType;

public class CreateQueueTypeCommandHandler(IQueueTypeRepository queueTypeRepository,ILogger<CreateQueueTypeCommandHandler> _logger) : IRequestHandler<CreateQueueTypeCommand, Result>
{
    private static readonly TimeSpan UtcOffset = TimeSpan.FromHours(5);
    public async Task<Result> Handle(CreateQueueTypeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос на создание типа очереди");
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
            _logger.LogError($"Ошибка при создании типа очереди.");
            return Result.Failure<int>(new Error(Errors.BadRequest, "Ошибка добавления типа очереди"));
        }
        _logger.LogInformation($"Успешное создание типа очереди");
        return Result.Success();
    }
}
