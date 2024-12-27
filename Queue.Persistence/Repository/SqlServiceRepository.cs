using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Queue.Domain.Common.Exceptions;
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
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            var Service = await _dbContext.Services.FindAsync(id);
            if (Service is null)
            {
                return Result.Failure(new Error(Errors.NotFound, "Service not Found"));

            }
            _dbContext.Services.Remove(Service);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
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
            return Result.Failure<List<Service>>(new Error(Errors.InternalServerError, ex.Message));
        }
    }

    public async Task<Result> GetServiceById(int id)
    {
        try
        {
            var Service = await _dbContext.Services.FindAsync(id);
            if (Service is null)
            {
                return Result.Failure(new Error(Errors.NotFound, "Service not Found"));
            }
            return Result.Success(Service);
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));

        }
    }

    public async Task<Result> UpdateAsync(int serviceId, string? nameRu = null, string? nameKk = null, string? nameEn = null, string? descriptionRu = null, string? descriptionKk = null, string? descriptionEn = null, int? avarageExecutionTime = null, int? parentServiceId = null, int? queueTypeId = null)
    {
        try
        {
            var serviceUpdate = await _dbContext.Services.FindAsync(serviceId);
            if(serviceUpdate is null)
            {
                return Result.Failure(new Error(Errors.NotFound, "Service not Found"));
            }
            serviceUpdate.NameRu = nameRu?? serviceUpdate.NameRu;
            serviceUpdate.NameKk = nameKk?? serviceUpdate.NameKk;
            serviceUpdate.NameEn = nameEn?? serviceUpdate.NameEn;
            serviceUpdate.DescriptionRu =descriptionRu?? serviceUpdate.DescriptionRu;
            serviceUpdate.DescriptionKk = descriptionKk?? serviceUpdate.DescriptionKk;
            serviceUpdate.DescriptionEn = descriptionEn?? serviceUpdate.DescriptionEn;
            serviceUpdate.AverageExecutionTime = avarageExecutionTime?? serviceUpdate.AverageExecutionTime;
            serviceUpdate.ParentserviceId = parentServiceId?? serviceUpdate.ParentserviceId;
            serviceUpdate.QueueTypeId = queueTypeId?? serviceUpdate.QueueTypeId;
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch(Exception ex)
        {
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
        }
    }
}
