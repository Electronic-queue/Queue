using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.UserWindows.Commands.DeleteUserWindow;
public record DeleteUserWindowCommand(int UserWindowId):IRequest<Result>;
