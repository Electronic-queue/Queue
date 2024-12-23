using KDS.Primitives.FluentResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Record.Queries.GetRecodList
{
    public record GetRecordListQuery():IRequest<Result<List<Domain.Entites.Record>>>;
  
}
