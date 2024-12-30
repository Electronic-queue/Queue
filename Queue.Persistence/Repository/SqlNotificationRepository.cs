using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;
using System;
using System.Data;

namespace Queue.Persistence.Repository;

public class SqlNotificationRepository(QueuesDbContext _dbContext, ILogger<SqlNotificationRepository> logger) : INotificationRepository
{
    public async Task<Result> AddAsync(Notification notification)
    {
        try
        {
            logger.LogInformation("Добавление нового уведомления в базу данных.");
            await _dbContext.AddAsync(notification);
            await _dbContext.SaveChangesAsync();
            logger.LogInformation("Действие {TargetNameRu},{TargetNameKk},{TargetNameEn} добавлено в базу данных.", notification.NameRu, notification.NameKk, notification.NameEn);
            return Result.Success();
        }
        catch(Exception)
        {
            logger.LogError("Ошибка при добавлении уведомления {TargetNameRu},{TargetNameKk},{TargetNameEn} в базу данных.", notification.NameRu, notification.NameKk, notification.NameEn);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при добавлении уведомления {notification.NameRu}({notification.NameKk}, {notification.NameEn}) в базу данных."));
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            logger.LogInformation("Удаление уведомления из базы данных.");
            var notification = await _dbContext.Notifications.FindAsync(id);
            if (notification != null)
            {
                _dbContext.Notifications.Remove(notification);
                await _dbContext.SaveChangesAsync();
                logger.LogInformation("Уведомление с id {TargetId} удалена из базы данных.", id);
                return Result.Success();
            }
            logger.LogError("Уведомление с id  {TargetId} не найдена в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Уведомление с id {id} не найдена в базе данных."));
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при удалении уведомления с id {TargetId} из базы данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при удалении уведомления с id {id} из базы данных."));
        }
    }

    public async Task<Result<List<Notification>>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("Получение полного списка уыедомлений из базы данных.");
            var notification = await _dbContext.Notifications.ToListAsync();
            logger.LogInformation("Полный список уведомлений получен из базы данных.");
            return Result.Success(notification);
        }
        catch(Exception )
        {
            logger.LogError("Ошибка при получении полного списка уведомлений из базы данных.");
            return Result.Failure<List<Notification>>(new Error(Errors.InternalServerError, "Ошибка при получении полного списка уведомлений из базы данных."));
        }
    }

    public async Task<Result> GetNotificationdById(int id)
    {
        try
        {
            logger.LogInformation("Получение уведомления из базы данных.");
            var notification = await _dbContext.Notifications.FindAsync(id);
            if (notification != null)
            {
                logger.LogInformation("Уведомление с id {TargetId} получена из базы данных.", id);
                return Result.Success(notification);
            }
            logger.LogError("Уведомление с id {TargetId} не найдена в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Уведомление с id {id} не найдена в базе данных."));
        }
        catch (Exception )
        {
            logger.LogError("Ошибка при получении уведомления с id {TargetRoleId} из базы данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при получении уведомления с id {id} из базы данных."));
        }
    }

    public async Task<Result> UpdateAsync(int notificationId, int? notificationTypeId=null, string? nameRu = null, string? nameKk = null, string? nameEn = null, string? contentRu = null, string? contentKk = null, string? contentEn = null)
    {
        try
        {
            logger.LogInformation("Обновление уведомления в базе данных.");
            var notificationUpdate = await _dbContext.Notifications.FindAsync(notificationId);
            if(notificationUpdate != null)
            {
                notificationUpdate.NameRu = nameRu ?? notificationUpdate.NameRu;
                notificationUpdate.NameKk = nameKk ?? notificationUpdate.NameKk;
                notificationUpdate.NameEn = nameEn ?? notificationUpdate.NameEn;
                notificationUpdate.ContentRu = contentRu ?? notificationUpdate.ContentRu;
                notificationUpdate.ContentKk = contentKk ?? notificationUpdate.ContentKk;
                notificationUpdate.ContentEn = contentEn ?? notificationUpdate.ContentEn;
                notificationUpdate.NotificationTypeId = notificationTypeId ?? notificationUpdate.NotificationTypeId;
                await _dbContext.SaveChangesAsync();
                logger.LogInformation("Уведомление с id {TargetId} обновлена в базе данных.", notificationId);
                return Result.Success();
            }
            logger.LogError("Уведомление с id {TargetId} не найдена в базе данных.", notificationId);
            return Result.Failure<Role>(new Error(Errors.NotFound, $"Уведомление с id {notificationId} не найдена в базе данных."));

        }
        catch(Exception)
        {
            logger.LogError("Ошибка при обновлении уведомления с id {TargetId} в базе данных.", notificationId);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при обновлении уведомления с id {notificationId} в базе данных."));
        }
    }
}
