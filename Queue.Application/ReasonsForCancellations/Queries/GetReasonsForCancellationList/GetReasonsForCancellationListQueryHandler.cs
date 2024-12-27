using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.ReasonsForCancellations.Queries.GetReasonsForCancellationList;

public class GetReasonsForCancellationListQueryHandler(IReasonsForCancellationRepository reasonRepository) : IRequestHandler<GetReasonsForCancellationListQuery, Result<List<ReasonsForCancellation>>>
{
    public async Task<Result<List<ReasonsForCancellation>>> Handle(GetReasonsForCancellationListQuery request, CancellationToken cancellationToken)
    {
        var result = await reasonRepository.GetAllAsync();
        return Result.Success(result.Value);
    }
}
