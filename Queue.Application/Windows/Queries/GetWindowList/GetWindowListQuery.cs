using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Entites;

namespace Queue.Application.Windows.Queries.GetWindowList;

public class GetWindowListQuery:IRequest<Result<List<Window>>>
{
    public int WindowId { get; set; }
}
