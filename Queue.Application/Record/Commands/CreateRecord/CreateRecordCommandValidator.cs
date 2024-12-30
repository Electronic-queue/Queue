using FluentValidation;

namespace Queue.Application.Record.Commands.CreateRecord;

public class CreateRecordCommandValidator : AbstractValidator<CreateRecordCommand>
{
    public CreateRecordCommandValidator()
    {
        RuleFor(x => x.FirstName)
           .MaximumLength(100).WithMessage("FirstName не должно превышать 100 символов.")
           .NotEmpty().WithMessage("FirstName обязательно.");
        RuleFor(x => x.LastName)
           .MaximumLength(100).WithMessage("LastName не должно превышать 100 символов.")
           .NotEmpty().WithMessage("LastName обязательно.");
        RuleFor(x => x.Surname)
            .MaximumLength(500).WithMessage("Surname не должно превышать 500 символов.")
            .When(x => !string.IsNullOrEmpty(x.Surname));
        RuleFor(x => x.Iin)
            .NotEmpty().WithMessage("Поле Iin не должно быть пустым.")
            .Length(12).WithMessage("Поле Iin должно содержать ровно 12 символов.")
             .Matches(@"^\d+$").WithMessage("Поле Iin должно содержать только цифры.");
        RuleFor(x => x.RecordStatusId)
          .GreaterThan(0).WithMessage("RecordStatusId должен быть больше нуля.")
          .NotEmpty().WithMessage("RecordStatusId обязательно.");
        RuleFor(x => x.ServiceId)
          .GreaterThan(0).WithMessage("ServiceId должен быть больше нуля.")
          .NotEmpty().WithMessage("ServiceId обязательно.");
        RuleFor(x => x.IsCreatedByEmployee)
           .NotNull().WithMessage(" IsCreatedByEmployee Поле обязательно для заполнения.");
        RuleFor(x => x.CreatedBy)
            .GreaterThan(0).WithMessage("CreatedBy должен быть больше нуля.")
            .NotEmpty().WithMessage("CreatedBy обязательно.");
        RuleFor(x => x.TicketNumber)
            .GreaterThan(0).WithMessage("TicketNumber должен быть больше нуля")
            .NotEmpty().WithMessage("TicketNumber обязательно");

    }
}
