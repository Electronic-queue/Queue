using FluentValidation;

namespace Queue.Application.QueueTypes.Commands.DeleteQueueType;

public class DeleteQueueTypeCommandValidator:AbstractValidator<DeleteQueueTypeCommand>
{
    public DeleteQueueTypeCommandValidator()
    {
        RuleFor(x => x.QueueTypeId)
           .GreaterThan(0).WithMessage("QueueTypeId должен быть больше нуля.")
           .NotEmpty().WithMessage("QueueTypeId обязательно.");
    }
}
