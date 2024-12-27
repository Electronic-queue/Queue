using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.ExceedingsTimes.Queries.GetExceedingsTimeList;

public class GetExceedingsTimeListQueryHandler(IExceedingsTimeRepository timeRepository) : IRequestHandler<GetExceedingsTimeListQuery, Result<List<ExceedingsTime>>>
{
    public async Task<Result<List<ExceedingsTime>>> Handle(GetExceedingsTimeListQuery request, CancellationToken cancellationToken)
    {
        var result = await timeRepository.GetAllAsync();
        return Result.Success(result.Value);
    }
}
