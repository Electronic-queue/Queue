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

    public async Task<Result<Notification>> GetNotificationById(int id)
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

    public async Task<Result> UpdateAsync(Notification notification)
    {
        try
        {
            var notificationUpdate = await _dbContext.Notifications.FindAsync(notification.NotificationId);
            if(notificationUpdate is null)
            {
                return Result.Failure(new Error("Not Found", "Notification  not Found"));
            }
            notificationUpdate.NameRu = notification.NameRu;
            notificationUpdate.NameKk = notification.NameKk;
            notificationUpdate.NameEn = notification.NameEn;
            notificationUpdate.ContentRu = notification.ContentRu;
            notificationUpdate.ContentKk = notification.ContentKk;
            notificationUpdate.ContentEn = notification.ContentEn;
            notificationUpdate.NotificationTypeId = notification.NotificationTypeId;
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch(Exception ex)
        {
            return Result.Failure(new Error("Database Error", ex.Message));
        }
    }
}
