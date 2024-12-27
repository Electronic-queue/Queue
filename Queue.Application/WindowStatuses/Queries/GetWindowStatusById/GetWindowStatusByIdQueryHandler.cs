using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.WindowStatuses.Queries.GetWindowStatusById;

public  class GetWindowStatusByIdQueryHandler(IWindowStatusRepository windowStatusRepository):IRequestHandler<GetWindowStatusByIdQuery,Result>
{
    public async Task<Result> Handle (GetWindowStatusByIdQuery request, CancellationToken cancellationToken)
    {
        var windowStatus = await windowStatusRepository.GetWindowStatusById(request.WindowStatusId);
        if (windowStatus.IsFailed) 
        {
            return Result.Failure(new Error("405", "Error"));
        }
        return Result.Success();
    }
}
