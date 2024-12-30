using FluentValidation;

namespace Queue.Application.Record.Commands.DeleteRecord;

public class DeleteRecordCommandValidator:AbstractValidator<DeleteRecordCommand>
{
    public DeleteRecordCommandValidator()
    {
        RuleFor(x => x.RecordId)
           .GreaterThan(0).WithMessage("Id должен быть больше нуля.")
           .NotEmpty().WithMessage("Id обязательно.");
    }
}
