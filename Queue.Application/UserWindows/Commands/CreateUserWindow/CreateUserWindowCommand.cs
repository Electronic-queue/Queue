using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.UserWindows.Commands.CreateUserWindow;

public record CreateUserWindowCommand(
    int UserId=0,
    int WindowId=0,
    DateTime? StartTime=null,
    DateTime? EndTime=null,
    DateTime? CreatedOn=null,
    int? CreatedBy=null
    ):IRequest<Result>;
