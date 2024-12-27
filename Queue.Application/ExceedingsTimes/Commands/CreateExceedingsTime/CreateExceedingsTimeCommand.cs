using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.ExceedingsTimes.Commands.CreateExceedingsTime;

public record CreateExceedingsTimeCommand(
    int WindowId,
    int TimeForExcommunication
    ) :IRequest<Result>;
