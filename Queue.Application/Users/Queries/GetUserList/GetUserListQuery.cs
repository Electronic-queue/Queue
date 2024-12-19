using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Entites;

namespace Queue.Application.Users.Queries.GetUserList
{
    public class GetUserListQuery:IRequest<Result<List<User>>>
    {
        public int UserId {  get; set; }
    }
}
