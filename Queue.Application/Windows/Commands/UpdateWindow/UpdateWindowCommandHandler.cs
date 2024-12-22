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
            var window = new Window
            {
                WindowNumber = request.WindowNumber,
                CreatedBy = request.CreatedBy,
            };
            var entity=await windowRepository.UpdateAsync(window);
            if (entity.IsFailed)
            {
                return Result.Failure(new Error(Errors.BadRequest, "UpdateError"));
            }
            return Result.Success(entity);
        }
    }
}
