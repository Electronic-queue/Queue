using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.UserWindows.Queries.GetUserWindowList;

public class GetUserWindowQueryHandler(IUserWindowRepository userWIndowRepository) : IRequestHandler<GetUserWindowListQuery, Result<List<UserWindow>>>
{
    public async Task<Result<List<UserWindow>>> Handle(GetUserWindowListQuery request, CancellationToken cancellationToken)
    {
        var result = await userWIndowRepository.GetAllAsync();
        return Result.Success(result.Value);
    }
}
