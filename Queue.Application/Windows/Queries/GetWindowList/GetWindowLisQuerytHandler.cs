using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Windows.Queries.GetWindowList
{
    public class GetWindowLisQuerytHandler(IWindowRepository windowRepository):IRequestHandler<GetWindowListQuery,Result<List<Window>>>
    {
        public async Task<Result<List<Window>>> Handle(GetWindowListQuery request,CancellationToken cancellationToken)
        {
            var windowQuery = await windowRepository.GetAllAsync();
            var window = windowQuery.Value;
            return Result.Success(window);
        }
    }
}
