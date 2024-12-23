using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.Windows.Commands.DeleteWindow;

public record DeleteWindowCommand(int WindowId):IRequest<Result>;

