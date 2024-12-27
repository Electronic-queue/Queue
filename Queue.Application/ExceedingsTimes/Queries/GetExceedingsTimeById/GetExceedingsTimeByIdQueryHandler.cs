using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;

namespace Queue.Application.ExceedingsTimes.Queries.GetExceedingsTimeById;

public class GetExceedingsTimeByIdQueryHandler(IExceedingsTimeRepository timeRepository) : IRequestHandler<GetExceedingsTimeByIdQuery, Result>
{
    public async Task<Result> Handle(GetExceedingsTimeByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await timeRepository.GetExceedingsTimeById(request.ExceedingsTimeId);
        if(result.IsFailed)
        {
            return Result.Failure(new Error(Errors.BadRequest, "Ошибка при запросе"));
        }
        return Result.Success();
    }
}
