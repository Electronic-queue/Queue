using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.WindowStatuses.Queries.GetWindowStatusList;

public class GetWindowStatusListQueryHandler(IWindowStatusRepository windowStatusRepository) : IRequestHandler<GetWindowStatusListQuery, Result<List<WindowStatus>>>
{
    public async Task<Result<List<WindowStatus>>> Handle(GetWindowStatusListQuery request, CancellationToken cancellationToken)
    {
        var windowStatus = await windowStatusRepository.GetAllAsync();
        var windowStatusVal=windowStatus.Value;
        return Result.Success(windowStatusVal);
    }
}
