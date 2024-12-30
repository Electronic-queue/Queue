using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;

namespace Queue.Application.ReasonsForCancellations.Commands.UpdateReasonsForCancellation;

public class UpdateReasonsForCancellationCommandHandler(IReasonsForCancellationRepository reasonRepository,ILogger<UpdateReasonsForCancellationCommandHandler> _logger) : IRequestHandler<UpdateReasonsForCancellationCommand, Result>
{
    public async Task<Result> Handle(UpdateReasonsForCancellationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Обработка запроса на обновление причины для отмены в базе данных.");
        var result = await reasonRepository.UpdateAsync(
            reasonId:request.ReasonId,
            recordId:request.RecordId,
            explanation:request.Explantation
            );
        if(result.IsFailed)
        {
            _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на обновление причины для отмены в базе данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        _logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();

    }
}
