using FluentValidation;

namespace Queue.Application.Record.Commands.UpdateRecord;

public class UpdateRecordCommandValidator:AbstractValidator<UpdateRecordCommand>
{
    public UpdateRecordCommandValidator()
    { 
        RuleFor(x => x.RecordId)
               .GreaterThan(0).WithMessage("Id должен быть больше нуля.")
               .NotEmpty().WithMessage("Id обязательно.");
        RuleFor(x => x.FirstName)
           .MaximumLength(100).WithMessage("FirstName не должно превышать 100 символов.")
           .When(x => !string.IsNullOrEmpty(x.FirstName));
        RuleFor(x => x.LastName)
           .MaximumLength(100).WithMessage("LastName не должно превышать 100 символов.")
           .When(x => !string.IsNullOrEmpty(x.LastName));
        RuleFor(x => x.Surname)
            .MaximumLength(500).WithMessage("Surname не должно превышать 500 символов.")
            .When(x => !string.IsNullOrEmpty(x.Surname));
        RuleFor(x => x.Iin)
            .Length(12).When(x => !string.IsNullOrEmpty(x.Iin)).WithMessage("Поле Iin должно содержать ровно 12 символов.")
            .Matches(@"^\d+$").When(x => !string.IsNullOrEmpty(x.Iin)).WithMessage("Поле Iin должно содержать только цифры.");
        RuleFor(x => x.RecordStatusId)
            .GreaterThan(0).When(x => x.RecordStatusId.HasValue)
            .WithMessage("RecordStatusId должен быть больше нуля");
        RuleFor(x => x.ServiceId)
          .GreaterThan(0).
          When(x => x.ServiceId.HasValue).WithMessage("ServiceId должен быть больше нуля.");

        RuleFor(x => x.IsCreatedByEmployee)
            .NotNull().When(x => x.IsCreatedByEmployee.HasValue).WithMessage("Поле IsCreatedByEmployee обязательно для заполнения, если оно указано.");
        RuleFor(x => x.CreatedBy)
            .GreaterThan(0).When(x => x.CreatedBy.HasValue).WithMessage("CreatedBy должен быть больше нуля.")


        RuleFor(x => x.TicketNumber)
            .GreaterThan(0).When(x => x.TicketNumber.HasValue).WithMessage("TicketNumber должен быть больше нуля");
            
    }
}
