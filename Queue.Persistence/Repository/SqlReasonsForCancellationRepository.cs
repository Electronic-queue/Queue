using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Persistence.Repository;

public class SqlReasonsForCancellationRepository(QueuesDbContext _dbContext) : IReasonsForCancellationRepository
{
    public async Task<Result> AddAsync(ReasonsForCancellation reasonsForCancellation)
    {
        try
        {
            await _dbContext.ReasonsForCancellations.AddAsync(reasonsForCancellation);
            await _dbContext.SaveChangesAsync();
            return Result.Success();

        }
        catch (Exception ex)
        {
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            var reason = await _dbContext.ReasonsForCancellations.FindAsync(id);
            if (reason is null)
            {
                return Result.Failure(new Error(Errors.NotFound, "Reason not Found"));
            }
            _dbContext.ReasonsForCancellations.Remove(reason);
            await _dbContext.SaveChangesAsync();
            return Result.Success();

        }
        catch(Exception ex)
        {
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
        }
    }

    public async Task<Result<List<ReasonsForCancellation>>> GetAllAsync()
    {
        try
        {
            var result = await _dbContext.ReasonsForCancellations.ToListAsync();
            return Result.Success(result);
        }
        catch(Exception ex)
        {
            return Result.Failure<List<ReasonsForCancellation>>(new Error(Errors.InternalServerError, ex.Message));
        }
    }

    public async Task<Result> GetReasonsForCancellationById(int id)
    {
        try
        {
            var reason = await _dbContext.ReasonsForCancellations.FindAsync(id);
            if(reason is null)
            {
                return Result.Failure(new Error(Errors.NotFound, "Reason not Found"));
            }
            return Result.Success();
        }
        catch(Exception ex)
        {
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
        }
    }

    public async Task<Result> UpdateAsync(int reasonId, int? recordId = null, string? explanation = null)
    {
        try
        {
            var reason = await _dbContext.ReasonsForCancellations.FindAsync(reasonId);
            if(reason is null)
            {
                return Result.Failure(new Error(Errors.NotFound, "Reason not Found"));
            }
            reason.RecordId = recordId ?? reason.RecordId;
            reason.Explanation = explanation ?? reason.Explanation;
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch(Exception ex)
        {
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
        }
    }
}
