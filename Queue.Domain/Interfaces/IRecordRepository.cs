using KDS.Primitives.FluentResult;
using Queue.Domain.Entites;

namespace Queue.Domain.Interfaces;

public interface IRecordRepository
{
    Task<Result<List<Record>>> GetAllAsync();
    Task<Result> AddAsync(Record record);
    Task<Result> DeleteAsync(int id);
    Task<Result> GetRecordById(int id);
    Task<Result> UpdateAsync(int recordId, string? firstName=null,string? lastName = null, string? surName = null, string? iin = null, int? recordStatusId = null, int? serviceId = null, bool? isCreatedByEmployee = null, int? createdBy = null, int? ticketNumber = null);
}
