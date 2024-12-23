using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Entites;

namespace Queue.Application.Services.Queries.GetServiceList;

public class GetServiceListQuery : IRequest<Result<List<Service>>>
{
}
