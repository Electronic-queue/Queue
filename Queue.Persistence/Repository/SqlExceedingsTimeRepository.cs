using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Persistence.Repository;

public class SqlExceedingsTimeRepository(QueuesDbContext _dbContext) : IExceedingsTimeRepository
{
    public async Task<Result> AddAsync(ExceedingsTime exceedingsTime)
    {
        try
        {
            await _dbContext.ExceedingsTimes.AddAsync(exceedingsTime);
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
            var exceedingsTime = await _dbContext.ExceedingsTimes.FindAsync(id);
            if (exceedingsTime is null)
            {
                return Result.Failure(new Error(Errors.NotFound, "ExceedingsTime not Found"));
            }
            _dbContext.ExceedingsTimes.Remove(exceedingsTime);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch(Exception ex)
        {
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
        }
    }

    public async Task<Result<List<ExceedingsTime>>> GetAllAsync()
    {
        try
        {
            var exceedingsTime = await _dbContext.ExceedingsTimes.ToListAsync();
            return Result.Success(exceedingsTime);
        }
        catch (Exception ex)
        {
            return Result.Failure<List<ExceedingsTime>>(new Error(Errors.InternalServerError, ex.Message));
        }
    }

    public async Task<Result> GetExceedingsTimeById(int id)
    {
        try
        {
            var exceedingsTime = await _dbContext.ExceedingsTimes.FindAsync(id);
            if(exceedingsTime is null)
            {
                return Result.Failure(new Error(Errors.NotFound, "ExceedingsTime not Found"));
            }
            return Result.Success(exceedingsTime);
        }
        catch(Exception ex)
        {
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
        }
    }

    public async Task<Result> UpdateAsync(int exceedingsTimeId, int? windowId = null, int? timeForExcommunication = null,DateTime? canceledOn=null)
    {
        try
        {
            var exceedingsTime = await _dbContext.ExceedingsTimes.FindAsync(exceedingsTimeId);
             if(exceedingsTime is null)
            {
                return Result.Failure(new Error(Errors.NotFound, "ExceedingsTime not Found"));
            }
            exceedingsTime.WindowId = windowId ?? exceedingsTime.WindowId;
            exceedingsTime.TimeForExcommunication = timeForExcommunication ?? exceedingsTime.TimeForExcommunication;
            exceedingsTime.CanceledOn = canceledOn ?? exceedingsTime.CanceledOn;
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch(Exception ex)
        {
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
        }
    }
}
