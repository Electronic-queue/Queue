using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Queue.Application.Users.Commands.DeleteUser;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Windows.Commands.DeleteWindow
{
    public record DeleteWindowCommandHandler(IWindowRepository windowRepository, ILogger<DeleteUserCommandHandler> _logger) :IRequestHandler<DeleteWindowCommand,Result>
    {
        public async Task<Result> Handle(DeleteWindowCommand request,CancellationToken cancellationToken)
        {
            _logger.LogInformation("Запрос на удалениие пользователя");
            var entity = await windowRepository.DeleteAsync(request.WindowId);
            if (entity.IsFailed)
            {
                return Result.Failure(new Error(Errors.BadRequest, "DeleteError"));
            }
            return Result.Success(entity);
        }
    }
}
