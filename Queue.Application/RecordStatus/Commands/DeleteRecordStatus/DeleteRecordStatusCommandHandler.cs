using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.RecordStatus.Commands.DeleteRecordStatus
{
    public class DeleteRecordStatusCommandHandler(IRecordStatusRepository recordStatusRepository):IRequestHandler<DeleteRecordStatusCommand,Result>
    {
        public async Task<Result> Handle(DeleteRecordStatusCommand request,CancellationToken cancellationToken)
        {
            var recordStatus = await recordStatusRepository.DeleteAsync(request.RecordStatusId);
            if (recordStatus.IsFailed)
            {
                return Result.Failure(new Error(Errors.BadRequest, "DeleteError"));
            }
            return Result.Success();

        }
    }
}
