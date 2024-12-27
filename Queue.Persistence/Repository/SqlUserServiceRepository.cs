using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Persistence.Repository;

public class SqlUserServiceRepository(QueuesDbContext _dbContext) : IUserServiceRepository
{
    public async Task<Result> AddAsync(UserService userService)
    {
        try
        {
            await _dbContext.UserServices.AddAsync(userService);
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
            var userService = await _dbContext.UserServices.FindAsync(id);
            if (userService is null)
            {
                return Result.Failure(new Error(Errors.NotFound, "UserService not Found"));
            }
            _dbContext.Remove(userService);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch(Exception ex)
        {
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
        }
    }

    public async Task<Result<List<UserService>>> GetAllAsync()
    {
        try
        {
            var userService = await _dbContext.UserServices.ToListAsync();
            return Result.Success(userService);
        }
        catch (Exception ex)
        {
            return Result.Failure<List<UserService>>(new Error(Errors.InternalServerError, ex.Message));
        }
    }

    public async Task<Result> GetUserServiceById(int id)
    {
        try
        {
            var userService = await _dbContext.UserServices.FindAsync(id);
            if(userService is null)
            {
                return Result.Failure(new Error(Errors.NotFound, "UserService not Found"));
            }
            return Result.Success();
        }
        catch(Exception ex)
        {
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
        }
    }

    public async Task<Result> UpdateAsync(int userServiceId, int? userId = null, int? serviceId = null, string? descriptionRu = null, string? descriptionKk = null, string? description = null, bool? isActive = null)
    {
        try
        {
            var userService = await _dbContext.UserServices.FindAsync(userServiceId);
            if(userService is null)
            {
                return Result.Failure(new Error(Errors.NotFound, "UserService not Found"));
            }
            userService.UserId = userId ?? userService.UserId;
            userService.ServiceId = serviceId ?? userService.ServiceId;
            userService.DescriptionRu = descriptionRu ?? userService.DescriptionRu;
            userService.DescriptionKk = descriptionKk ?? userService.DescriptionKk;
            userService.DescriptionEn = description ?? userService.DescriptionEn;
            userService.IsActive = isActive ?? userService.IsActive;
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch(Exception ex)
        {
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
        }

    }
}
