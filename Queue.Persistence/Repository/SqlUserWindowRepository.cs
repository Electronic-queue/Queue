using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Persistence.Repository;

public class SqlUserWindowRepository(QueuesDbContext _dbContext) : IUserWindowRepository
{
    public async Task<Result> AddAsync(UserWindow userWindow)
    {
        try
        {
            await _dbContext.UserWindows.AddAsync(userWindow);
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
            var userWindow = await _dbContext.UserWindows.FindAsync(id);
            if (userWindow is null)
            {
                return Result.Failure(new Error(Errors.NotFound, "UserService not Found"));
            }
            _dbContext.UserWindows.Remove(userWindow);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch(Exception ex)
        {
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
        }
    }

    public async Task<Result<List<UserWindow>>> GetAllAsync()
    {
        try
        {
            var userWindow = await _dbContext.UserWindows.ToListAsync();
            return Result.Success(userWindow);
        }
        catch(Exception ex)
        {
            return Result.Failure<List<UserWindow>>(new Error(Errors.InternalServerError, ex.Message));
        }
    }

    public async Task<Result> GetUserWindowById(int id)
    {
        try
        {
            var userWindow = await _dbContext.UserWindows.FindAsync(id);
            if(userWindow is null)
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

    public async Task<Result> UpdateAsync(int userWindowId, int? userId = null, int? windowId = null)
    {
        try
        {
            var userWindow = await _dbContext.UserWindows.FindAsync(userWindowId);
            if(userWindow is null)
            {
                return Result.Failure(new Error(Errors.NotFound, "UserService not Found"));
            }
          
            userWindow.UserId = userId ?? userWindow.UserId;
            userWindow.WindowId = windowId ?? userWindow.WindowId;
           
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
        }
    }
}
