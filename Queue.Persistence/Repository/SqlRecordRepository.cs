using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Persistence.Repository;

public class SqlRecordRepository(QueuesDbContext _dbContext) : IRecordRepository
{
    public async Task<Result> AddAsync(Record record)
    {
        try
        {
            await _dbContext.Records.AddAsync(record);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("Database Error", ex.Message));
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            var record = await _dbContext.Records.FindAsync(id);
            if (record is null)
            {
                return Result.Failure(new Error("Not Found", "Record not Found"));
            }
            _dbContext.Records.Remove(record);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("Database Error", ex.Message));
        }
    }

    public async Task<Result<List<Record>>> GetAllAsync()
    {
        try
        {
            var record = await _dbContext.Records.ToListAsync();
            return Result.Success(record);
        }
        catch (Exception ex)
        {
            return Result.Failure<List<Record>>(new Error("Database Error", ex.Message));
        }
    }

    public async Task<Result> GetRecordById(int id)
    {
        try
        {
            var record = await _dbContext.Records.FindAsync(id);
            if (record is null)
            {
                return Result.Failure(new Error("Not Found", "Record not Found"));
            }
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure<List<Record>>(new Error("Database Error", ex.Message));
        }
    }

    public async Task<Result> UpdateAsync(int recordId, string? firstName = null,string ? lastName = null, string? surName = null, string? iin = null, int? recordStatusId = null, int? serviceId = null, bool? isCreatedByEmployee = null, int? createdBy = null, int? ticketNumber = null)
    {
        try
        {
            var recordUpdate = await _dbContext.Records.FindAsync(recordId);
            if (recordUpdate is null)
            {
                return Result.Failure(new Error("Not Found", "Record Not Found"));
            }

            recordUpdate.FirstName = firstName??recordUpdate.FirstName;
            recordUpdate.LastName = lastName ?? recordUpdate.LastName;
            recordUpdate.Surname = surName ?? recordUpdate.Surname;
            recordUpdate.Iin = iin?? recordUpdate.Iin;
            recordUpdate.RecordStatusId = recordStatusId?? recordUpdate.RecordStatusId;
            recordUpdate.ServiceId = serviceId?? recordUpdate.ServiceId;
            recordUpdate.IsCreatedByEmployee = isCreatedByEmployee?? recordUpdate.IsCreatedByEmployee;
            recordUpdate.CreatedBy = createdBy?? recordUpdate.CreatedBy;
            recordUpdate.TicketNumber = ticketNumber?? recordUpdate.TicketNumber;

            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure<List<Record>>(new Error("Database Error", ex.Message));
        }
    }
}
