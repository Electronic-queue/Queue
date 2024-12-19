using FluentValidation;

namespace Queue.Application.Users.Commands.DeleteUser;

public class DeleteUserCommandValidator:AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(deleteUserCommand =>
           deleteUserCommand.UserId).NotEqual(0);
    }
}
