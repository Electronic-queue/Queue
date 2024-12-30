using FluentValidation;

namespace Queue.Application.QueueTypes.Queries.GetQueueTypeById;

public class GetQueueTypeByIdQueryValidator:AbstractValidator<GetQueueTypeByIdQuery>
{
    public GetQueueTypeByIdQueryValidator()
    {
        RuleFor(x => x.QueueTypeId)
           .GreaterThan(0).WithMessage("QueueTypeId должен быть больше нуля.")
           .NotEmpty().WithMessage("QueueTypeId обязательно.");
    }
}
}
