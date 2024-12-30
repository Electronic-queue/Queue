using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Windows.Commands.UpdateWindow
{
    public class UpdateWindowCommandHandler(IWindowRepository windowRepository,ILogger<UpdateWindowCommandHandler> _logger):IRequestHandler<UpdateWindowCommand,Result>
    {
        public async Task<Result> Handle(UpdateWindowCommand request,CancellationToken cancellationToken)
        {
            _logger.LogInformation("Обработка запроса на обновление окна в базе данных.");
            var result = await windowRepository.UpdateAsync(
                windowId: request.WindowId,
                windowNumber: request.WindowNumber,
                windowStatusId: request.WindowStatusId,
                createdBy:request.CreatedBy
                );
           
            if (result.IsFailed)
            {
                _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на обновление окна в базе данных.", result.Error.Code);
                return Result.Failure(result.Error);
            }
            _logger.LogInformation("Запрос успешно обработан.");
            return Result.Success();
        }
    }
}
