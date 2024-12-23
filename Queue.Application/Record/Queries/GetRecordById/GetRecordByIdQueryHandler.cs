using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Interfaces;

namespace Queue.Application.Record.Queries.GetRecordById;

public class GetRecordByIdQueryHandler(IRecordRepository recordRepository):IRequestHandler<GetRecordByIdQuery,Result>
{
    public async Task<Result> Handle (GetRecordByIdQuery request,CancellationToken cancellationToken)
    {
        var record = await recordRepository.GetRecordById(request.RecordId);
        if(record is null)
        {
            return Result.Failure(new Error("405", "Error"));
        }
        return Result.Success();
    }
}
