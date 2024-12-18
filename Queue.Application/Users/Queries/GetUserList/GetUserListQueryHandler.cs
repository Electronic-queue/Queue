using AutoMapper;
using AutoMapper.QueryableExtensions;
using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.Users.Queries.GetUserList
{
    public class GetUserListQueryHandler(IUserRepository _userRepository, IMapper _mapper) :
        IRequestHandler<GetUserListQuery, Result<List<User>>>
    {


        public async Task<Result<List<User>>> Handle(GetUserListQuery request,
            CancellationToken cancellationToken)
        {
            var usersQuery = await _userRepository.GetAllAsync();
            var users=usersQuery.Value;
            return Result.Success(users);
        }

    }
}
