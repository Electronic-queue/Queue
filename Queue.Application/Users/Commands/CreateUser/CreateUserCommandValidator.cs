using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Users.Commands.CreateUser
{
    public  class CreateUserCommandValidator:AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(createUserCommand =>
            createUserCommand.Iin).NotEmpty();
            RuleFor(createUserCommand=>
            createUserCommand.FirstName).MaximumLength(250).NotEmpty();
        }
    }
}
