using FluentValidation;
using Queue.Application.Common.Validators;

namespace Queue.Application.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x =>  x.Iin).SetValidator(new UserIinValidator());
        RuleFor(createUserCommand =>
        createUserCommand.FirstName).MaximumLength(250).NotEmpty();
    }
}
