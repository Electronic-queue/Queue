using KDS.Primitives.FluentResult;
using Queue.Domain.Entites;

namespace Queue.Domain.Interfaces;

public interface IServiceRepository
{
    Task<Result<List<Service>>> GetAllAsync();
    Task<Result> AddAsync(Service service);
    Task<Result> DeleteAsync(int id);
    Task<Result> GetServiceById(int id);
    Task<Result> UpdateAsync(int serviceId,string? nameRu=null,string? nameKk=null,string? nameEn=null,string? descriptionRu=null, string? descriptionKk=null,string? descriptionEn=null,int? avarageExecutionTime=null,int? parentServiceId=null,int? queueTypeId=null);
}
