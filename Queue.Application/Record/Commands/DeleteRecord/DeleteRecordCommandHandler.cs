using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Record.Commands.DeleteRecord;

public class DeleteRecordCommandHandler(IRecordRepository recordRepository):IRequestHandler<DeleteRecordCommand,Result>
{
    public async Task<Result> Handle(DeleteRecordCommand request,CancellationToken cancellationToken)
    {
        var record=await recordRepository.DeleteAsync(request.RecordId);
        if (record is null)
        {
            return Result.Failure(new Error(Errors.BadRequest, "DeleteError"));
        }
        return Result.Success();
    }
}
