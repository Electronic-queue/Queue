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
        _logger.LogInformation("Обработка запроса на удаление услуги из базы данных.");
        var result = await _serviceRepository.DeleteAsync(request.ServiceId);
        if (result.IsFailed)
        {
            _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на удаление услуги из базы данных.", result.Error.Code);
            return Result.Failure(result.Error);
        }
        _logger.LogInformation("Запрос успешно обработан.");
        return result;
    }
}
