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

namespace Queue.Application.Windows.Commands.CreateWindow
{
    public class CreateWindowCommandHandler(IWindowRepository _windowRepository):IRequestHandler<CreateWindowCommand,Result>
    {
        private static readonly TimeSpan UtcOffset = TimeSpan.FromHours(5);
        public async Task<Result> Handle(CreateWindowCommand request,CancellationToken cancellationToken)
        {
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
                return Result.Failure<int>(new Error(Errors.BadRequest, "Ошибка добавления"));
            }
            return Result.Success();
        }
    }
}
