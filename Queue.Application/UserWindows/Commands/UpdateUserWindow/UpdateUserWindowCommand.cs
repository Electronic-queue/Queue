using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.UserWindows.Commands.UpdateUserWindow;

public record UpdateUserWindowCommand(
    int UserWindowId,
    int? UserId=null ,
    int? WindowId = null
    ) :IRequest<Result>;
