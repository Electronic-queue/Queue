using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Interfaces;

namespace Queue.Application.QueueTypes.Queries.GetQueueTypeById;

public class GetQueueTypeByIdQueryHandler(IQueueTypeRepository queueTypeRepository):IRequestHandler<GetQueueTypeByIdQuery, Result>
{
    public async Task<Result> Handle(GetQueueTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var queueType = await queueTypeRepository.GetQueueTypedById(request.QueueTypeId);
        if(queueType.IsFailed)
        {
            return Result.Failure(new Error("405", "Error"));
        }
        return Result.Success();
    }
}
