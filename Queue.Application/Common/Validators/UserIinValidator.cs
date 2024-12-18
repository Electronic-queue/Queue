using FluentValidation;

namespace Queue.Application.Common.Validators;

public class UserIinValidator : AbstractValidator<string>
{
    public UserIinValidator()
    {
        RuleFor(x => x).NotEmpty().WithMessage("ИИн не может быть меньше 12").MaximumLength(12).WithMessage("ИИн не может быть меньше 12");
    }
}
