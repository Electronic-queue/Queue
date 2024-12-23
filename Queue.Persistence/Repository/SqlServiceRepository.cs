using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Persistence.Repository;

public class SqlServiceRepository(QueuesDbContext _dbContext) : IServiceRepository
{
    public async Task<Result> AddAsync(Service Service)
    {
        try
        {
            await _dbContext.AddAsync(Service);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch(Exception ex)
        {
            return Result.Failure(new Error("Database Error", ex.Message));
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            var Service = await _dbContext.Services.FindAsync(id);
            if (Service is null)
            {
                return Result.Failure(new Error("Not Found", "Service not Found"));

            }
            _dbContext.Services.Remove(Service);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("Database Error",ex.Message));
        }
    }

    public async Task<Result<List<Service>>> GetAllAsync()
    {
        try
        {
            var Service = await _dbContext.Services.ToListAsync();
            return Result.Success(Service);
        }
        catch(Exception ex)
        {
            return Result.Failure<List<Service>>(new Error("Database Error", ex.Message));
        }
    }

    public async Task<Result<Service>> GetServiceById(int id)
    {
        try
        {
            var Service = await _dbContext.Services.FindAsync(id);
            if (Service is null)
            {
                return (Result<Service>)Result.Failure(new Error("Not Found", "Service not Found"));
            }
            return Result.Success(Service);
        }
        catch (Exception ex)
        {
            return (Result<Service>)Result.Failure(new Error("Database Error", ex.Message));

        }
    }

    public async Task<Result> UpdateAsync(Service Service)
    {
        try
        {
            var ServiceUpdate = await _dbContext.Services.FindAsync(Service.ServiceId);
            if(ServiceUpdate is null)
            {
                return Result.Failure(new Error("Not Found", "Service  not Found"));
            }
            ServiceUpdate.NameRu = Service.NameRu;
            ServiceUpdate.NameKk = Service.NameKk;
            ServiceUpdate.NameEn = Service.NameEn;
            ServiceUpdate.DescriptionRu = Service.DescriptionRu;
            ServiceUpdate.DescriptionKk = Service.DescriptionKk;
            ServiceUpdate.DescriptionEn = Service.DescriptionEn;
            ServiceUpdate.AverageExecutionTime = Service.AverageExecutionTime;
            ServiceUpdate.ParentserviceId = Service.ParentserviceId;
            ServiceUpdate.QueueTypeId = Service.QueueTypeId;
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch(Exception ex)
        {
            return Result.Failure(new Error("Database Error", ex.Message));
        }
    }
}
