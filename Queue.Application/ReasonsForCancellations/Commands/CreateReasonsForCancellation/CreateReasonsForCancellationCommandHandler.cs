using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.ReasonsForCancellations.Commands.CreateReasonsForCancellation;

public class CreateReasonsForCancellationCommandHandler(IReasonsForCancellationRepository reasonRepository,ILogger<CreateReasonsForCancellationCommandHandler> _logger) : IRequestHandler<CreateReasonsForCancellationCommand, Result>
{
    private static readonly TimeSpan UtcOffset = TimeSpan.FromHours(5);
    public async Task<Result> Handle(CreateReasonsForCancellationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос на создание причины отмены");
        var reason = new ReasonsForCancellation
        {
            RecordId=request.RecordId,
            Explanation=request.Explantation,
            CreatedOn = DateTimeOffset.UtcNow.ToOffset(UtcOffset).DateTime
        };

        var result=await reasonRepository.AddAsync(reason);
        if (result.IsFailed)
        {
            _logger.LogError($"Ошибка при создании причины для отмены.");
            return Result.Failure<int>(new Error(Errors.BadRequest, "Ошибка добавления причины для отмены"));
        }
        _logger.LogInformation($"Успешное создание причины для отмены");
        return Result.Success();
    }
}
