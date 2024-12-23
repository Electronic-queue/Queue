using KDS.Primitives.FluentResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Windows.Commands.CreateWindow
{
    public  record CreateWindowCommand(
        int WindowNumber=0,
       int WindowStatusId=0,
        int? CreatedBy = null
        ):IRequest<Result>;
   
}
