using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;
using System.Data;

namespace Queue.Persistence.Repository;

public class SqlNotificationTypeRepository(QueuesDbContext _dbContext, ILogger<SqlNotificationTypeRepository> logger) : INotificationTypeRepository
{
    public async Task<Result> AddAsync(NotificationType notificationType)
    {

        try
        {
            logger.LogInformation("Добавление нового типа уведомления в базу данных.");
            await _dbContext.AddAsync(notificationType);
            await _dbContext.SaveChangesAsync();
            logger.LogInformation("Тип уведомления {TargetNameRu}({TargetNameKk}, {TargetNameEn}) добавлена в базу данных.", notificationType.NameRu, notificationType.NameKk, notificationType.NameEn);
            return Result.Success();
        }
        catch(Exception)
        {
            logger.LogError("Ошибка при добавлении типа уведомления {TargetNameRu}({TargetNameKk}, {TargetNameEn}) в базу данных.", notificationType.NameRu, notificationType.NameKk, notificationType.NameEn);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при добавлении типа уведомления {notificationType.NameRu}({notificationType.NameKk}, {notificationType.NameEn}) в базу данных."));
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            logger.LogInformation("Удаление типа уведомления из базы данных.");
            var notificationType = await _dbContext.NotificationTypes.FindAsync(id);
            if (notificationType != null)
            {
                _dbContext.NotificationTypes.Remove(notificationType);
                await _dbContext.SaveChangesAsync();
                logger.LogInformation("Тип уведомления  с id {TargetId} удалена из базы данных.", id);
                return Result.Success();

            }
            logger.LogError("Тип уведомления  с id {TargetId} не найдена в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Тип уведомления  с id {id} не найдена в базе данных."));

        }
        catch (Exception)
        {
            logger.LogError("Ошибка при удалении типа уведомления  с id {TargetId} из базы данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при удалении типа уведомления  с id {id} из базы данных."));
        }
    }
    public async Task<Result<List<NotificationType>>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("Получение полного списка типов уведомлении из базы данных.");
            var notificationType = await _dbContext.NotificationTypes.ToListAsync();
            logger.LogInformation("Полный список типов уведомлении получен из базы данных.");
            return Result.Success(notificationType);
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при получении полного списка типов уведомлении из базы данных.");
            return Result.Failure<List<NotificationType>>(new Error(Errors.InternalServerError, "Ошибка при получении полного списка типов уведомлении из базы данных."));

        }
    }
    public async Task<Result> GetNotificationTypeById(int id)
    {
        try
        {
            logger.LogInformation("Получение типа уведомления из базы данных.");
            var notificationType = await _dbContext.NotificationTypes.FindAsync(id);
            if (notificationType != null)
            {
                logger.LogInformation("Тип уведомления с id {TargetId} получен из базы данных.", id);
                return Result.Success(notificationType);
            }
            logger.LogError("Тип уведомления с id {TargetId} не найден в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Тип уведомления с id {id} не найден в базе данных."));
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при получении типа уведомления с id {TargetId} из базы данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при получении типа уведомления с id {id} из базы данных."));
        }
    }

    public async Task<Result> UpdateAsync(int notificationTypeId, string? nameRu = null, string? nameKk = null, string? nameEn = null, string? descriptionRu = null, string? descriptionKk = null, string? descriptionEn = null)
    {
        try
        {
            logger.LogInformation("Обновление типа уведомления в базе данных.");
            var notificationTypeUpdate = await _dbContext.NotificationTypes.FindAsync(notificationTypeId);
            if(notificationTypeUpdate != null)
            {
                notificationTypeUpdate.NameRu = nameRu ?? notificationTypeUpdate.NameRu;
                notificationTypeUpdate.NameKk = nameKk ?? notificationTypeUpdate.NameKk;
                notificationTypeUpdate.NameEn = nameEn ?? notificationTypeUpdate.NameEn;
                notificationTypeUpdate.DescriptionRu = descriptionRu ?? notificationTypeUpdate.DescriptionRu;
                notificationTypeUpdate.DescriptionKk = descriptionKk ?? notificationTypeUpdate.DescriptionKk;
                notificationTypeUpdate.DescriptionEn = descriptionEn ?? notificationTypeUpdate.DescriptionEn;
                await _dbContext.SaveChangesAsync();
                logger.LogInformation("Тип уведомления с id {TargetId} обновлена в базе данных.", notificationTypeId);
                return Result.Success();
            }
            logger.LogError("Тип уведомления с id {TargetId} не найдена в базе данных.", notificationTypeId);
            return Result.Failure<Role>(new Error(Errors.NotFound, $"Тип уведомления с id {notificationTypeId} не найдена в базе данных."));

        }
        catch(Exception)
        {
            logger.LogError("Ошибка при обновлении типа уведомления с id {TargetId} в базе данных.", notificationTypeId);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при обновлении типа уведомления с id {notificationTypeId} в базе данных."));
        }
    }
}
