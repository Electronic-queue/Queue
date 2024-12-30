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

namespace Queue.Application.Windows.Commands.CreateWindow
{
    public class CreateWindowCommandHandler(IWindowRepository _windowRepository,ILogger<CreateWindowCommandHandler> _logger):IRequestHandler<CreateWindowCommand,Result>
    {
        private static readonly TimeSpan UtcOffset = TimeSpan.FromHours(5);
        public async Task<Result> Handle(CreateWindowCommand request,CancellationToken cancellationToken)
        {
            _logger.LogInformation("Обработка запроса на создание нового окна в базе данных.");
            var window = new Window
            {
                WindowNumber = request.WindowNumber,
                WindowStatusId = request.WindowStatusId,
                CreatedBy = request.CreatedBy,
                CreatedOn = DateTimeOffset.UtcNow.ToOffset(UtcOffset).DateTime
            };
            var result =await _windowRepository.AddAsync(window);
            if(result is null)
            {
                _logger.LogError("Ошибка [{ErrorCode}] при обработке запроса на создание нового окна в базе данных.", result.Error.Code);
                return Result.Failure(result.Error);
            }
            _logger.LogInformation("Запрос успешно обработан.");
            return Result.Success();
        }
    }
}
