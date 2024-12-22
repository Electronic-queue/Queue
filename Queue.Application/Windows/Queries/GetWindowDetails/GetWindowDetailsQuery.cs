using KDS.Primitives.FluentResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Windows.Queries.GetWindowDetails
{
    public class GetWindowDetailsQuery: IRequest<Result>
    {
        public int WindowId {  get; set; }
    }
}
