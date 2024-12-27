using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;

namespace Queue.Application.Services.Commands.DeleteService;

public class DeleteServiceCommandHandler(IServiceRepository _serviceRepository, ILogger<DeleteServiceCommandHandler> _logger) : IRequestHandler<DeleteServiceCommand, Result>
{
    public async Task<Result> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос на удалениие услуги");
        var result = await _serviceRepository.DeleteAsync(request.ServiceId);
        if (result is null || result.IsFailed)
        {
            _logger.LogError($"Ошибка при удалении услуги с id: {request.ServiceId}.");
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка удаления услуги."));
        }
        _logger.LogInformation($"Успешное удаление услуги с id: {request.ServiceId}.");
        return result;
    }
}
