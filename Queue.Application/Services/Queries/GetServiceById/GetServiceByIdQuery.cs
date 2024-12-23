using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Entites;

namespace Queue.Application.Services.Queries.GetServiceById;

public class GetServiceByIdQuery : IRequest<Result<Service>>
{
    public int ServiceId { get; set; }
}
