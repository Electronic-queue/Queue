using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.Windows.Queries.GetWindowDetails;

public record GetWindowDetailsQuery(int WindowId): IRequest<Result>;
