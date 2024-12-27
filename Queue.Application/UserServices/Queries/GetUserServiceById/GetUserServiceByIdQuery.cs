using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.UserServices.Queries.GetUserServiceById;

public record GetUserServiceByIdQuery(int UserServiceId):IRequest<Result>;