using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Windows.Queries.GetWindowDetails
{
    public class GetWindowDetailsQueryHandler(IWindowRepository windowRepository):IRequestHandler<GetWindowDetailsQuery,Result>
    {
        public async Task<Result> Handle(GetWindowDetailsQuery request,CancellationToken cancellationToken)
        {
            var result=await windowRepository.GetWindowById(request.WindowId);
            if (result == null)
            {
                return Result.Failure(new Error("405", "Error"));
            }
            return Result.Success();
        }
    }
}
