using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Interfaces;

namespace Queue.Application.UserWindows.Queries.GetUserWindowById;

public class GetUserWindowByIdQueryHandler(IUserWindowRepository userWIndowRepository) : IRequestHandler<GetUserWindowByIdQuery, Result>
{
    public async Task<Result> Handle(GetUserWindowByIdQuery request, CancellationToken cancellationToken)
    {
        var userWindow = await userWIndowRepository.GetUserWindowById(request.UserWindowId);
        return Result.Success();
    }
}
