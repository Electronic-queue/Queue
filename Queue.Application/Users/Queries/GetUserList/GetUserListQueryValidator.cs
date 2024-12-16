using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Users.Queries.GetUserList
{
    public class GetUserListQueryValidator:AbstractValidator<GetUserListQuery>
    {
        public GetUserListQueryValidator()
        {
            RuleFor(user => user.Id).NotEmpty();
        }
    }
}
