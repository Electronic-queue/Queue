using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Interfaces;

namespace Queue.Application.ExceedingsTimes.Commands.DeleteExceedingsTime;

public record DeleteExceedingsTimeCommand(int ExceedingsTimeId ):IRequest<Result>;

