using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;

namespace Queue.Application.Windows.Commands.DeleteWindow;

public record DeleteWindowCommandHandler(IWindowRepository windowRepository, ILogger<DeleteWindowCommandHandler> _logger) :IRequestHandler<DeleteWindowCommand,Result>
{
    public async Task<Result> Handle(DeleteWindowCommand request,CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос на удалениие пользователя");
        var entity = await windowRepository.DeleteAsync(request.WindowId);
        if (entity.IsFailed)
        {
            return Result.Failure(new Error(Errors.BadRequest, "DeleteError"));
        }
        return Result.Success();
    }
}
