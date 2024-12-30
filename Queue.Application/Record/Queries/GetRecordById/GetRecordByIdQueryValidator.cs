using FluentValidation;

namespace Queue.Application.Record.Queries.GetRecordById;

public class GetRecordByIdQueryValidator:AbstractValidator<GetRecordByIdQuery>
{
	public GetRecordByIdQueryValidator()
	{
        RuleFor(x => x.RecordId)
           .GreaterThan(0).WithMessage("Id должен быть больше нуля.")
           .NotEmpty().WithMessage("Id обязательно.");
    }
}
