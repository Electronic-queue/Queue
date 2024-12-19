using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Users.Queries.GetUserDetails
{
    public class GetUserDetailsQueryValidator:AbstractValidator<GetUserDetailsQuery>

    {
        public GetUserDetailsQueryValidator()
        {
            RuleFor(user=>user.UserId).NotEqual(0);
        }
    }
}
