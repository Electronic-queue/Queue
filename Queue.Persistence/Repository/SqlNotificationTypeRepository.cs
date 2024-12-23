using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Persistence.Repository;

public class SqlNotificationTypeRepository(QueuesDbContext _dbContext) : INotificationTypeRepository
{
    public async Task<Result> AddAsync(NotificationType notificationType)
    {
        try
        {
            await _dbContext.AddAsync(notificationType);
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
            var notificationType = await _dbContext.NotificationTypes.FindAsync(id);
            if (notificationType is null)
            {
                return Result.Failure(new Error("Not Found", "Notification Type not Found"));

            }
            _dbContext.NotificationTypes.Remove(notificationType);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("Database Error",ex.Message));
        }
    }

    public async Task<Result<List<NotificationType>>> GetAllAsync()
    {
        try
        {
            var notificationType = await _dbContext.NotificationTypes.ToListAsync();
            return Result.Success(notificationType);
        }
        catch(Exception ex)
        {
            return Result.Failure<List<NotificationType>>(new Error("Database Error", ex.Message));
        }

    }

    public async Task<Result<NotificationType>> GetNotificationTypeById(int id)
    {
        try
        {
            var notificationType = await _dbContext.NotificationTypes.FindAsync(id);
            if (notificationType is null)
            {
                return (Result<NotificationType>)Result.Failure(new Error("Not Found", "Notification Type not Found"));
            }
            return Result.Success(notificationType);
        }
        catch (Exception ex)
        {
            return (Result<NotificationType>)Result.Failure(new Error("Database Error", ex.Message));

        }
    }

    public async Task<Result> UpdateAsync(NotificationType notificationType)
    {
        try
        {
            var notificationTypeUpdate = await _dbContext.NotificationTypes.FindAsync(notificationType.NotificationTypeId);
            if(notificationTypeUpdate is null)
            {
                return Result.Failure(new Error("Not Found", "Notification Type not Found"));
            }
            notificationTypeUpdate.NameRu = notificationType.NameRu;
            notificationTypeUpdate.NameKk = notificationType.NameKk;
            notificationTypeUpdate.NameEn = notificationType.NameEn;
            notificationTypeUpdate.DescriptionRu = notificationType.DescriptionRu;
            notificationTypeUpdate.DescriptionKk = notificationType.DescriptionKk;
            notificationTypeUpdate.DescriptionEn = notificationType.DescriptionEn;
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch(Exception ex)
        {
            return Result.Failure(new Error("Database Error", ex.Message));
        }
    }
}
