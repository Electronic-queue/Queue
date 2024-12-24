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

    public async Task<Result> UpdateAsync(int notificationTypeId, string? nameRu = null, string? nameKk = null, string? nameEn = null, string? descriptionRu = null, string? descriptionKk = null, string? descriptionEn = null)
    {
        try
        {
            var notificationTypeUpdate = await _dbContext.NotificationTypes.FindAsync(notificationTypeId);
            if(notificationTypeUpdate is null)
            {
                return Result.Failure(new Error("Not Found", "Notification Type not Found"));
            }
            notificationTypeUpdate.NameRu = nameRu?? notificationTypeUpdate.NameRu;
            notificationTypeUpdate.NameKk = nameKk?? notificationTypeUpdate.NameKk;
            notificationTypeUpdate.NameEn = nameEn?? notificationTypeUpdate.NameEn;
            notificationTypeUpdate.DescriptionRu = descriptionRu?? notificationTypeUpdate.DescriptionRu;
            notificationTypeUpdate.DescriptionKk = descriptionKk?? notificationTypeUpdate.DescriptionKk;
            notificationTypeUpdate.DescriptionEn = descriptionEn?? notificationTypeUpdate.DescriptionEn;
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch(Exception ex)
        {
            return Result.Failure(new Error("Database Error", ex.Message));
        }
    }
}
