using KDS.Primitives.FluentResult;
using Queue.Domain.Entites;

namespace Queue.Domain.Interfaces;

public interface IRecordStatusRepository
{
    Task<Result<List<RecordStatus>>> GetAllAsync();
    Task<Result> AddAsync(RecordStatus recordStatus);
    Task<Result> DeleteAsync(int id);
    Task<Result> GetRecordStatusById(int id);
    Task<Result> UpdateAsync(int recordStatusId,string? nameRu=null,string? nameKk=null,string? nameEn=null,string? descriptionRu=null,string? descriptionKk=null,string? descriptionEn=null,int? createdBy=null);
}
