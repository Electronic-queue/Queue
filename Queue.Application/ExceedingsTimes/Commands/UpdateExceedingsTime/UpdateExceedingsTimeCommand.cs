using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.ExceedingsTimes.Commands.UpdateExceedingsTime;

public record UpdateExceedingsTimeCommand(
    int ExceedingsTimeId,
    int? WindowId=null,
    int? TimeForExcommunication=null,
    DateTime? CanceledOn=null
    ) :IRequest<Result>;

