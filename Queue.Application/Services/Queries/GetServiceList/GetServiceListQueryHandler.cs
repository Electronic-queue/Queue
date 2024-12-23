using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;

namespace Queue.Application.Services.Queries.GetServiceList;

public class GetServiceListQueryHandler(IServiceRepository _ServiceRepository) :
    IRequestHandler<GetServiceListQuery, Result<List<Service>>>
{
    public async Task<Result<List<Service>>> Handle(GetServiceListQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _ServiceRepository.GetAllAsync();
        if (result == null || result.IsFailed)
        {
            return (Result<List<Service>>)Result.Failure(new Error(Errors.NotAllowed, "Ошибка при запросе списка уведомлений."));
        }
        var Services = result.Value;
        return result;
    }

}
