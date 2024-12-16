using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Users.Commands.UpdateUser
{
     public class UpdateUserCommandValidator:AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(updateUserCommand =>
            updateUserCommand.Iin).NotEmpty();
            RuleFor(updateUserCommand =>
            updateUserCommand.FirstName).MaximumLength(250).NotEmpty();
            RuleFor(updateUserCommand =>
            updateUserCommand.Id).NotEqual(Guid.Empty);
        }
    }
}
