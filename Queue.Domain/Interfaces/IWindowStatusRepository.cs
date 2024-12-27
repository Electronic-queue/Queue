using KDS.Primitives.FluentResult;
using Queue.Domain.Entites;

namespace Queue.Domain.Interfaces;

public interface IWindowStatusRepository
{
    Task<Result> GetWindowStatusById(int id);
    Task<Result<List<WindowStatus>>> GetAllAsync();
    Task<Result> AddAsync(WindowStatus windowStatus);
    Task<Result> DeleteAsync(int id);
    Task<Result> UpdateAsync(int windowStatusId,string?nameRu=null,string?nameKk=null,string? nameEn=null,string?descriptionRu=null,string? descriptionKk=null,string? descriptionEn=null);
}
