using KDS.Primitives.FluentResult;
using Queue.Domain.Entites;

namespace Queue.Domain.Interfaces;

public interface IExceedingsTimeRepository
{
    Task<Result<List<ExceedingsTime>>> GetAllAsync();
    Task<Result> AddAsync(ExceedingsTime exceedingsTime);
    Task<Result> DeleteAsync(int id);
    Task<Result> GetExceedingsTimeById(int id);
    Task<Result> UpdateAsync(int exceedingsTimeId,int? windowId=null,int? timeForExcommunication=null,DateTime? canceledOn=null);
}
