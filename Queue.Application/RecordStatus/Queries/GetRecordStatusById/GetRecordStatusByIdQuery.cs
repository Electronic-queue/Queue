using KDS.Primitives.FluentResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.RecordStatus.Queries.GetRecordStatusById
{
    public record GetRecordStatusByIdQuery(int RecordStatusId):IRequest<Result>;
    
}
