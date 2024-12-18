using KDS.Primitives.FluentResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Users.Queries.GetUserDetails
{
    public class GetUserDetailsQuery:IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
