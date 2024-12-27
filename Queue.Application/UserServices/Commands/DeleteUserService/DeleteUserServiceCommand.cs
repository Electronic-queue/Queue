using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.UserServices.Commands.DeleteUserService;

public record DeleteUserServiceCommand(int UserServiceId):IRequest<Result>;
