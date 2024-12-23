using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Persistence.Repository;

public class SqlWindowRepository(QueuesDbContext _dbContext) : IWindowRepository
{
    public async Task<Result> AddAsync(Window window)
    {
        try
        {
            await _dbContext.AddAsync(window);
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
            var window = await _dbContext.Windows.FindAsync(id);
            if (window is null)
            {
                return Result.Failure(new Error("Not Found", "Window not Found"));

            }
            _dbContext.Windows.Remove(window);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("Database Error", ex.Message));
        }



    }

    public async Task<Result<List<Window>>> GetAllAsync()
    {
        try
        {
            var window = await _dbContext.Windows.ToListAsync();
            return Result.Success(window);
        }
        catch (Exception ex)
        {
            return Result.Failure<List<Window>>(new Error("Database Error", ex.Message));
        }

    }

    public async Task<Result> GetWindowById(int id)
    {
        try
        {
            var window = await _dbContext.Windows.FindAsync(id);
            if (window is null)
            {
                return Result.Failure(new Error("Not Found", "Window not found"));
            }
            return Result.Success(window);
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("Database Error", ex.Message));

        }
    }

    public async Task<Result> UpdateAsync(int windowId, int? windowStatusId=null, int? windowNumber = null,int? createdBy=null)
    {

        try
        {
            var windowUpdate = await _dbContext.Windows.FindAsync(windowId);
            if (windowUpdate is null)
            {
                return Result.Failure(new Error("Not Found", "Window Not Found"));
            }
          
            windowUpdate.WindowNumber = windowNumber??windowUpdate.WindowNumber;
            windowUpdate.WindowStatusId = windowStatusId?? windowUpdate.WindowStatusId;
            windowUpdate.CreatedBy=createdBy??windowUpdate.CreatedBy;

            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("Database Error", ex.Message));
        }
    }
}
