using FluentValidation;
using Queue.Application.Common.Validators;

namespace Queue.Application.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(createUserCommand =>
        createUserCommand.FirstName).MaximumLength(250).NotEmpty();
    }
}
