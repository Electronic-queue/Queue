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
        _logger.LogInformation("Запрос на обновление причины отмены");
        var result = await reasonRepository.UpdateAsync(
            reasonId:request.ReasonId,
            recordId:request.RecordId,
            explanation:request.Explantation
            );
        if(result.IsFailed)
        {
            _logger.LogError($"Ошибка при обновлении причины отмены с id: {request.ReasonId}.");
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка обновления причины отмены"));
        }
        _logger.LogInformation($"Успешное обновления причины отмены с id: {request.ReasonId}.");
        return Result.Success();

    }
}
