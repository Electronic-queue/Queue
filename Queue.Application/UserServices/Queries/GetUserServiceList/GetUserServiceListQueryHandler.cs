using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.UserServices.Queries.GetUserServiceList;

public class GetUserServiceListQueryHandler(IUserServiceRepository userServiceRepository) : IRequestHandler<GetUserServiceListQuery,Result<List<UserService>>>
{ 
    public async Task<Result<List<UserService>>> Handle(GetUserServiceListQuery request, CancellationToken cancellationToken)
    {
        var result = await userServiceRepository.GetAllAsync();
        var resultVal = result.Value;
        return Result.Success(resultVal);
    }
}
