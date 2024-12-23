using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.Services.Commands.DeleteService;

public record DeleteServiceCommand(int ServiceId) :IRequest<Result>;
