using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.UserServices.Queries.GetUserServiceById;

public class GetUserServiceByIdQueryHandler(IUserServiceRepository userServiceRepository) : IRequestHandler<GetUserServiceByIdQuery, Result>
{
    public async Task<Result> Handle(GetUserServiceByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await userServiceRepository.GetUserServiceById(request.UserServiceId);
        if (result.IsFailed) 
        {
            return Result.Failure<Service>(new Error(Errors.NotAllowed, "Ошибка при запросе"));
        }
        return Result.Success();
    }
}
