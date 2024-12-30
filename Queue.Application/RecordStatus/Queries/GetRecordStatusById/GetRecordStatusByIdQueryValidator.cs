using FluentValidation;

namespace Queue.Application.RecordStatus.Queries.GetRecordStatusById;

public class GetRecordStatusByIdQueryValidator:AbstractValidator<GetRecordStatusByIdQuery>
{
	public GetRecordStatusByIdQueryValidator()
	{
        RuleFor(x => x.RecordStatusId)
           .GreaterThan(0).WithMessage("Id должен быть больше нуля.")
           .NotEmpty().WithMessage("Id обязательно.");
    }
}
