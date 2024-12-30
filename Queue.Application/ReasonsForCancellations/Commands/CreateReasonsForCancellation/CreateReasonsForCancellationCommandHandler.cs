using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;
using Serilog.Core;

namespace Queue.Application.ReasonsForCancellations.Commands.CreateReasonsForCancellation;

public class CreateReasonsForCancellationCommandHandler(IReasonsForCancellationRepository reasonRepository,ILogger<CreateReasonsForCancellationCommandHandler> _logger) : IRequestHandler<CreateReasonsForCancellationCommand, Result>
{
    private static readonly TimeSpan UtcOffset = TimeSpan.FromHours(5);
    public async Task<Result> Handle(CreateReasonsForCancellationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Обработка запроса на создание новой причины для отмены в базе данных.");
        var reason = new ReasonsForCancellation
        {
            RecordId=request.RecordId,
            Explanation=request.Explantation,
            CreatedOn = DateTimeOffset.UtcNow.ToOffset(UtcOffset).DateTime
        };

        var result=await reasonRepository.AddAsync(reason);
        if (result.IsFailed)
        {
            _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на создание новой причины для отмены в базе данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        _logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}
