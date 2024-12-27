using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;

namespace Queue.Application.ReasonsForCancellations.Queries.ReasonsForCancellationById;

public class GetReasonsForCancellationByIdQueryHandler(IReasonsForCancellationRepository reasonRepository) : IRequestHandler<GetReasonsForCancellationByIdQuery, Result>
{
    public async Task<Result> Handle(GetReasonsForCancellationByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await reasonRepository.GetReasonsForCancellationById(request.ReasonId);
        if (result.IsFailed)
        {
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка при запросе"));
        }
        return Result.Success();
    }
}
