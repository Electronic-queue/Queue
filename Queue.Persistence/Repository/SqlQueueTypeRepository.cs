using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Persistence.Repository;

public class SqlQueueTypeRepository(QueuesDbContext _dbContext) : IQueueTypeRepository
{
    public async Task<Result> AddAsync(QueueType queueType)
    {
        try
        {
            await _dbContext.QueueTypes.AddAsync(queueType);
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
            var queueType = await _dbContext.QueueTypes.FindAsync(id);
            if(queueType is null)
            {
                return Result.Failure(new Error(Errors.NotFound, "QueueType not Found"));
            }
            _dbContext.QueueTypes.Remove(queueType);
            await _dbContext.SaveChangesAsync();
            return Result.Success();    
        }
        catch(Exception ex)
        {
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
        }
    }

    public async Task<Result<List<QueueType>>> GetAllAsync()
    {
        try
        {
            var queueType = await _dbContext.QueueTypes.ToListAsync();
            await _dbContext.SaveChangesAsync();
            return Result.Success(queueType);
        }
        catch(Exception ex)
        {
            return Result.Failure<List<QueueType>>(new Error(Errors.InternalServerError, ex.Message));
        }
    }

    public async Task<Result> GetQueueTypedById(int id)
    {
        try
        {
            var queueType = await _dbContext.QueueTypes.FindAsync(id);
            if(queueType is null)
            {
                return Result.Failure(new Error(Errors.NotFound, "QueueType not Found"));
            }
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch(Exception ex) 
        {
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
        }

    }

    public async Task<Result> UpdateAsync(int queueTypeId, string? nameRu = null, string? nameKk = null, string? nameEn = null, string? decriptionRu = null, string? decriptionKk = null, string? decriptionEn = null)
    {
        try
        {
            var queueType = await _dbContext.QueueTypes.FindAsync(queueTypeId);
            if (queueType is null)
            {
                return Result.Failure(new Error(Errors.NotFound, "QueueType not Found"));
            }
            queueType.NameRu = nameRu ?? queueType.NameRu;
            queueType.NameKk = nameKk ?? queueType.NameKk;
            queueType.NameEn = nameEn ?? queueType.NameEn;
            queueType.DescriptionRu = decriptionRu ?? queueType.DescriptionRu;
            queueType.DescriptionKk = decriptionKk ?? queueType.DescriptionKk;
            queueType.DescriptionEn = decriptionEn ?? queueType.DescriptionEn;
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch(Exception ex)
        {
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
        }
        
    }
}
