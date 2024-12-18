using AutoMapper;
using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.Users.Queries.GetUserDetails
{
    public class GetUsersDetailsQueryHandler(IUserRepository _userRepository,IMapper _mapper):
        IRequestHandler<GetUserDetailsQuery,Result>
    {
         
        public async Task<Result> Handle(GetUserDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetUserDetails(request.Id);
            if (result == null)
            {
                return Result.Failure(new Error("405", "Error"));
            }
            return Result.Success(result);
        }
    }
}
