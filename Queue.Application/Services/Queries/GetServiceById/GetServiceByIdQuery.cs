using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Entites;

namespace Queue.Application.Services.Queries.GetServiceById;

public record GetServiceByIdQuery(int ServiceId) : IRequest<Result>;
