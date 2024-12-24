using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Persistence.Repository;

public class SqlNotificationRepository(QueuesDbContext _dbContext) : INotificationRepository
{
    public async Task<Result> AddAsync(Notification notification)
    {
        try
        {
            await _dbContext.AddAsync(notification);
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
            var notification = await _dbContext.Notifications.FindAsync(id);
            if (notification is null)
            {
                return Result.Failure(new Error("Not Found", "Notification not Found"));

            }
            _dbContext.Notifications.Remove(notification);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("Database Error",ex.Message));
        }
    }

    public async Task<Result<List<Notification>>> GetAllAsync()
    {
        try
        {
            var notification = await _dbContext.Notifications.ToListAsync();
            return Result.Success(notification);
        }
        catch(Exception ex)
        {
            return Result.Failure<List<Notification>>(new Error("Database Error", ex.Message));
        }
    }

    public async Task<Result> GetNotificationdById(int id)
    {
        try
        {
            var notification = await _dbContext.Notifications.FindAsync(id);
            if (notification is null)
            {
                return (Result<Notification>)Result.Failure(new Error("Not Found", "Notification not Found"));
            }
            return Result.Success(notification);
        }
        catch (Exception ex)
        {
            return (Result<Notification>)Result.Failure(new Error("Database Error", ex.Message));

        }
    }

    public async Task<Result> UpdateAsync(int notificationId, int? notificationTypeId=null, string? nameRu = null, string? nameKk = null, string? nameEn = null, string? contentRu = null, string? contentKk = null, string? contentEn = null)
    {
        try
        {
            var notificationUpdate = await _dbContext.Notifications.FindAsync(notificationId);
            if(notificationUpdate is null)
            {
                return Result.Failure(new Error("Not Found", "Notification  not Found"));
            }
            notificationUpdate.NameRu = nameRu??notificationUpdate.NameRu;
            notificationUpdate.NameKk = nameKk??notificationUpdate.NameKk;
            notificationUpdate.NameEn = nameEn??notificationUpdate.NameEn;
            notificationUpdate.ContentRu = contentRu??notificationUpdate.ContentRu;
            notificationUpdate.ContentKk = contentKk??notificationUpdate.ContentKk;
            notificationUpdate.ContentEn = contentEn??notificationUpdate.ContentEn;
            notificationUpdate.NotificationTypeId = notificationTypeId??notificationUpdate.NotificationTypeId;
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch(Exception ex)
        {
            return Result.Failure(new Error("Database Error", ex.Message));
        }
    }
}
