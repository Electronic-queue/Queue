using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Persistence.Repository;

public class SqlWindowStatusRepository(QueuesDbContext _dbContext) : IWindowStatusRepository
{
    public async Task<Result> AddAsync(WindowStatus windowStatus)
    {
        try
        {
            await _dbContext.WindowStatuses.AddAsync(windowStatus);
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
            var windowStatus = await _dbContext.WindowStatuses.FindAsync(id);
            if (windowStatus is null)
            {
                return Result.Failure(new Error(Errors.NotFound, "WindowStatus not Found"));
            }
            _dbContext.WindowStatuses.Remove(windowStatus);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch(Exception ex) 
        {
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
        }
        

    }


    public async Task<Result<List<WindowStatus>>> GetAllAsync()
    {
        try
        {
            var windowStatuses=await _dbContext.WindowStatuses.ToListAsync();
            await _dbContext.SaveChangesAsync();
            return Result.Success(windowStatuses);
        }
        catch (Exception ex)
        {
            return Result.Failure<List<WindowStatus>>(new Error(Errors.InternalServerError, ex.Message));
        }
    }

    public async Task<Result> GetWindowStatusById(int id)
    {
        try
        {
            var windowStatus = await _dbContext.WindowStatuses.FindAsync(id);
            if(windowStatus is null)
            {
                return Result.Failure(new Error(Errors.NotFound, "WindowStatus not Found"));
            }
            return Result.Success(windowStatus);

        }
        catch(Exception ex)
        {
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
        }
    }

    public async Task<Result> UpdateAsync(int windowStatusId, string? nameRu = null, string? nameKk = null, string? nameEn = null, string? descriptionRu = null, string? descriptionKk = null, string? descriptionEn = null)
    {
        try
        {
            var windowStatus = await _dbContext.WindowStatuses.FindAsync(windowStatusId);
            if (windowStatus is null)
            {
                return Result.Failure(new Error(Errors.NotFound, "WindowStatus not Found"));
            }
            windowStatus.NameRu = nameRu ?? windowStatus.NameRu;
            windowStatus.NameKk = nameKk ?? windowStatus.NameKk;
            windowStatus.NameEn = nameEn ?? windowStatus.NameEn;
            windowStatus.DescriptionRu = descriptionRu ?? windowStatus.DescriptionRu;
            windowStatus.DescriptionKk = descriptionKk ?? windowStatus.DescriptionKk;
            windowStatus.DescriptionEn = descriptionEn ?? windowStatus.DescriptionEn;
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
        }
    }
}
