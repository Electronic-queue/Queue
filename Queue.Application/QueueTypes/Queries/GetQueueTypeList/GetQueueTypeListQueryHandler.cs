using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.QueueTypes.Queries.GetQueueTypeList;

public class GetQueueTypeListQueryHandler(IQueueTypeRepository queueTypeRepository) : IRequestHandler<GetQueueTypeListQuery, Result<List<QueueType>>>
{
    public async Task<Result<List<QueueType>>> Handle(GetQueueTypeListQuery request, CancellationToken cancellationToken)
    {
        var result = await queueTypeRepository.GetAllAsync();
        var resultVal = result.Value;
        return Result.Success(resultVal);
    }
}
