using FluentValidation;

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
