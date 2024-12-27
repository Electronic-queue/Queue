using KDS.Primitives.FluentResult;
using Queue.Domain.Entites;

namespace Queue.Domain.Interfaces;

public  interface IQueueTypeRepository
{
    Task<Result<List<QueueType>>> GetAllAsync();
    Task<Result> AddAsync(QueueType queueType);
    Task<Result> DeleteAsync(int id);
    Task<Result> GetQueueTypedById(int id);
    Task<Result> UpdateAsync(int queueTypeId,string?nameRu=null, string? nameKk = null, string? nameEn = null,string? decriptionRu=null, string? decriptionKk = null,string ? decriptionEn = null);
}
