using FluentValidation;

namespace Queue.Application.RecordStatus.Commands.DeleteRecordStatus;

public class DeleteRecordStatusCommandValidator:AbstractValidator<DeleteRecordStatusCommand>
{
    public DeleteRecordStatusCommandValidator()
    {
        RuleFor(x => x.RecordStatusId)
           .GreaterThan(0).WithMessage("Id должен быть больше нуля.")
           .NotEmpty().WithMessage("Id обязательно.");
    }
}
