using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Entites;

namespace Queue.Application.UserServices.Queries.GetUserServiceList;

public record GetUserServiceListQuery():IRequest<Result<List<UserService>>>;
