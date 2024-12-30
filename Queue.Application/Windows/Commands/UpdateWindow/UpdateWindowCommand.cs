using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.Windows.Commands.UpdateWindow;

public record UpdateWindowCommand(int WindowId,
    int? WindowNumber=null,
     int? WindowStatusId= null,
        int? CreatedBy = null) :IRequest<Result>;
