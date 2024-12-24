using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.Services.Queries.GetServiceById;

public class GetServiceByIdQueryHandler(IServiceRepository _serviceRepository) :
    IRequestHandler<GetServiceByIdQuery, Result>
{
    public async Task<Result> Handle(GetServiceByIdQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _serviceRepository.GetServiceById(request.ServiceId);
        if (result.IsFailed)
        {
            return Result.Failure<Service>(new Error(Errors.NotAllowed, "Ошибка при запросе услуги."));
        }
        return Result.Success();
    }
}
