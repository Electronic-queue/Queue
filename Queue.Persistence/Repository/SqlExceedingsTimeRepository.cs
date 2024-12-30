using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;
using System;
using static System.Collections.Specialized.BitVector32;

namespace Queue.Persistence.Repository;

public class SqlExceedingsTimeRepository(QueuesDbContext _dbContext, ILogger<SqlExceedingsTimeRepository> logger) : IExceedingsTimeRepository
{
    public async Task<Result> AddAsync(ExceedingsTime exceedingsTime)
    {

        try
        {
            logger.LogInformation("Добавление нового действия в базу данных.");
            await _dbContext.ExceedingsTimes.AddAsync(exceedingsTime);
            await _dbContext.SaveChangesAsync();
            logger.LogInformation("Время перерыва {TargetId} добавлено в базу данных.", exceedingsTime.ExceedingsTimeId);
            return Result.Success();
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при добавлении времени перерыва {TargetId} в базу данных.", exceedingsTime.ExceedingsTimeId);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при добавлении времени перерыва {exceedingsTime.ExceedingsTimeId} в базу данных."));
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            logger.LogInformation("Удаление времени перерыва из базы данных.");
            var exceedingsTime = await _dbContext.ExceedingsTimes.FindAsync(id);
            if (exceedingsTime != null)
            {
                _dbContext.ExceedingsTimes.Remove(exceedingsTime);
                await _dbContext.SaveChangesAsync();
                logger.LogInformation("Время перерыва с id {TargetId} удалено из базы данных.", id);
                return Result.Success();
            }
            logger.LogError("Время перерыва с id {TargetId} не найдено в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Время перерыва с id {id} не найдено в базе данных."));

        }
        catch
        {
            logger.LogError("Ошибка при удалении времени перерыва  с id {TargetId} из базы данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при удалении времени перерыва с id {id} из базы данных."));
        }
    }

    public async Task<Result<List<ExceedingsTime>>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("Получение полного списка времени перерываов из базы данных.");
            var exceedingsTime = await _dbContext.ExceedingsTimes.ToListAsync();
            logger.LogInformation("Полный список времени перерывов получен из базы данных.");
            return Result.Success(exceedingsTime);
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при получении полного списка времени перерывов из базы данных.");
            return Result.Failure<List<ExceedingsTime>>(new Error(Errors.InternalServerError, "Ошибка при получении полного списка времени перерывов из базы данных."));
        }
    }

    public async Task<Result> GetExceedingsTimeById(int id)
    {
        try
        {
            logger.LogInformation("Получение времени перерыва из базы данных.");
            var exceedingsTime = await _dbContext.ExceedingsTimes.FindAsync(id);
            if (exceedingsTime != null)
            {
                logger.LogInformation("Время перерыва  с id {TargetId} получено из базы данных.", id);
                return Result.Success(exceedingsTime);

            }
            logger.LogError("Время перерыва с id {TargetId} не найдено в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Время перерыва с id {id} не найдено в базе данных."));

        }
        catch (Exception)
        {
            logger.LogError("Ошибка при получении времени перерыва с id {TargetId} из базы данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при получении времени перерыва с id {id} из базы данных."));
        }
    }

    public async Task<Result> UpdateAsync(int exceedingsTimeId, int? windowId = null, int? timeForExcommunication = null, DateTime? canceledOn = null)
    {
        try
        {
            logger.LogInformation("Обновление времени перерыва в базе данных.");
            var exceedingsTime = await _dbContext.ExceedingsTimes.FindAsync(exceedingsTimeId);
            if (exceedingsTime != null)
            {
                exceedingsTime.WindowId = windowId ?? exceedingsTime.WindowId;
                exceedingsTime.TimeForExcommunication = timeForExcommunication ?? exceedingsTime.TimeForExcommunication;
                exceedingsTime.CanceledOn = canceledOn ?? exceedingsTime.CanceledOn;
                await _dbContext.SaveChangesAsync();
                logger.LogInformation("Время перерыва с id {TargetActionId} обновлено в базе данных.", exceedingsTimeId);
                return Result.Success();
            }
            logger.LogError("Время перерыва с id {TargetId} не найдено в базе данных.", exceedingsTimeId);
            return Result.Failure(new Error(Errors.NotFound, $"Время перерыва с id {exceedingsTimeId} не найдено в базе данных."));
        }
        catch
        {
            logger.LogError("Ошибка при обновлении времени перерыва с id {TargetId} в базе данных.", exceedingsTimeId);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при обновлении времени перерыва с id {exceedingsTimeId} в базе данных."));
        }
    }
}
