using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.WindowStatuses.Commands.CreateWindowStatus;

public class CreateWindowStatusCommandHandler(IWindowStatusRepository windowStatusRepository,ILogger<CreateWindowStatusCommandHandler> _logger):IRequestHandler<CreateWindowStatusCommand,Result>
{
    private static readonly TimeSpan UtcOffset = TimeSpan.FromHours(5);
    public async Task<Result> Handle(CreateWindowStatusCommand request,CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос на создание статуса окна");
        var windowStatus = new WindowStatus
        {
            NameRu = request.NameRu,
            NameKk = request.NameKk,
            NameEn = request.NameEn,
            DescriptionRu = request.DescriptionRu,
            DescriptionKk = request.DescriptionKk,
            DescriptionEn = request.DescriptionEn,
            CreatedOn = DateTimeOffset.UtcNow.ToOffset(UtcOffset).DateTime,
            CreatedBy = request.CreatedBy,
        };
        var result=await windowStatusRepository.AddAsync(windowStatus);
        if(result.IsFailed)
        {
            _logger.LogError($"Ошибка при создании статуса окна.");
            return Result.Failure<int>(new Error(Errors.BadRequest, "Ошибка добавления окна"));
        }
        _logger.LogInformation($"Успешное создание статуса окна");
        return Result.Success();

    }
}
