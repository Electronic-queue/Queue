using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Interfaces;

namespace Queue.Application.RecordStatus.Queries.GetRecordStatusList;

public class GetRecordStatusListQueryHandler(IRecordStatusRepository recordStatusRepository):IRequestHandler<GetRecordStatusListQuery,Result<List<Domain.Entites.RecordStatus>>>
{
    public async Task<Result<List<Domain.Entites.RecordStatus>>> Handle(GetRecordStatusListQuery request,CancellationToken cancellationToken)
    {
        var result = await recordStatusRepository.GetAllAsync();
        var resultVal = result.Value;
        return Result.Success(resultVal);
    }
}
