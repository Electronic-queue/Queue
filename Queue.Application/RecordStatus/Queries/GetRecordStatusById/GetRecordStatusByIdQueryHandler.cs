using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Interfaces;

namespace Queue.Application.RecordStatus.Queries.GetRecordStatusById;

public class GetRecordStatusByIdQueryHandler(IRecordStatusRepository recordStatusRepository):IRequestHandler<GetRecordStatusByIdQuery,Result>
{
    public async Task<Result> Handle(GetRecordStatusByIdQuery request,CancellationToken cancellationToken)
    {
        var recordStatus = await recordStatusRepository.GetRecordStatusById(request.RecordStatusId);
        if (recordStatus is null) 
        {
            return Result.Failure(new Error("405", "Error"));
        }
        return Result.Success();
    }
}
