using KDS.Primitives.FluentResult;
using MediatR;
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
    public class UpdateWindowCommandHandler(IWindowRepository windowRepository):IRequestHandler<UpdateWindowCommand,Result>
    {
        public async Task<Result> Handle(UpdateWindowCommand request,CancellationToken cancellationToken)
        {
            var window = await windowRepository.UpdateAsync(
                windowId: request.WindowId,
                windowNumber: request.WindowNumber,
                windowStatusId: request.WindowStatusId,
                createdBy:request.CreatedBy
                );
           
            if (window.IsFailed)
            {
                return Result.Failure(new Error(Errors.BadRequest, "UpdateError"));
            }
            return Result.Success();
        }
    }
}
