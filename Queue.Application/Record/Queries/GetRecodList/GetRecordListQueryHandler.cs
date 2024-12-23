using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Record.Queries.GetRecodList
{
    public class GetRecordListQueryHandler(IRecordRepository recordRepository):IRequestHandler<GetRecordListQuery,Result<List<Domain.Entites.Record>>>
    {
        public async Task<Result<List<Domain.Entites.Record>>> Handle(GetRecordListQuery request,CancellationToken cancellationToken)
        {
            var result=await recordRepository.GetAllAsync();
            var resultVal = result.Value;
            return Result.Success(resultVal);
        }
    }
}
