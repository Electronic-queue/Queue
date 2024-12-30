using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;
using Serilog.Core;

namespace Queue.Application.ReasonsForCancellations.Commands.DeleteReasonsForCancellation;

public class DeleteReasonsForCancellationCommandHandler(IReasonsForCancellationRepository reasonRepository,ILogger<DeleteReasonsForCancellationCommand> _logger) : IRequestHandler<DeleteReasonsForCancellationCommand, Result>
{
    public async Task<Result> Handle(DeleteReasonsForCancellationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Обработка запроса на удаление причины для отмены из базы данных.");
        var result = await reasonRepository.DeleteAsync(request.ReasonId);
        if (result.IsFailed)
        {
            _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на удаление причины для отмены из базы данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        _logger.LogInformation("Запрос успешно обработан.");
        return Result.Success();
    }
}
