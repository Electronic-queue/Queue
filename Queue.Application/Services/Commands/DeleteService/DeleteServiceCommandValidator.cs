using FluentValidation;

namespace Queue.Application.Services.Commands.DeleteService;

public class DeleteServiceCommandValidator : AbstractValidator<DeleteServiceCommand>
{
    public DeleteServiceCommandValidator()
    {
        RuleFor(x => x.ServiceId)
            .GreaterThan(0).WithMessage("ServiceId должен быть больше нуля.");
    }
}
