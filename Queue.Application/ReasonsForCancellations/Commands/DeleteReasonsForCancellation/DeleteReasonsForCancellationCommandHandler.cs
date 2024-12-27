using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;

namespace Queue.Application.ReasonsForCancellations.Commands.DeleteReasonsForCancellation;

public class DeleteReasonsForCancellationCommandHandler(IReasonsForCancellationRepository reasonRepository,ILogger<DeleteReasonsForCancellationCommand> _logger) : IRequestHandler<DeleteReasonsForCancellationCommand, Result>
{
    public async Task<Result> Handle(DeleteReasonsForCancellationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос на удалениие причины отмены");
        var result = await reasonRepository.DeleteAsync(request.ReasonId);
        if (result.IsFailed)
        {
            _logger.LogError($"Ошибка при удалении причины отмены с id: {request.ReasonId}.");
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка удаления причины отмены"));
        }
        _logger.LogInformation($"Успешное удаление причины отмены с id: {request.ReasonId}. ");
        return Result.Success();
    }
}
