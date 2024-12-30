using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;
using System;
using System.Data;

namespace Queue.Persistence.Repository;

public class SqlReasonsForCancellationRepository(QueuesDbContext _dbContext, ILogger<SqlReasonsForCancellationRepository> logger) : IReasonsForCancellationRepository
{
    public async Task<Result> AddAsync(ReasonsForCancellation reasonsForCancellation)
    {
        try
        {
            logger.LogInformation("Добавление нового времени перерыва в базу данных.");
            await _dbContext.ReasonsForCancellations.AddAsync(reasonsForCancellation);
            await _dbContext.SaveChangesAsync();
            logger.LogInformation("Время перерыва добавлена в базу данных.");
            return Result.Success();
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при добавлении времени перерыва в базу данных.");
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при добавлении нового времени перерыва в базу данных."));
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            logger.LogInformation("Удаление времени перерыва из базы данных.");
            var reason = await _dbContext.ReasonsForCancellations.FindAsync(id);
            if (reason != null)
            {
                _dbContext.ReasonsForCancellations.Remove(reason);
                await _dbContext.SaveChangesAsync();
                logger.LogInformation("Время перерыва с id {TargetId} удалено из базы данных.", id);
                return Result.Success();
            }
            logger.LogError("Время перерыва с id {TargetId} не найдено в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Время перерыва с id {id} не найдено в базе данных."));

        }
        catch(Exception)
        {
            logger.LogError("Ошибка при удалении времени перерыва с id {TargetId} из базы данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при удалении времени перерыва с id {id} из базы данных."));
        }
    }
    

    public async Task<Result<List<ReasonsForCancellation>>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("Получение полного списка времени перерывов из базы данных.");
            var result = await _dbContext.ReasonsForCancellations.ToListAsync();
            logger.LogInformation("Полный список времени перерывов получен из базы данных.");
            return Result.Success(result);
        }
        catch(Exception)
        {
            logger.LogError("Ошибка при получении полного списка времени перерывов из базы данных.");
            return Result.Failure<List<ReasonsForCancellation>>(new Error(Errors.InternalServerError, "Ошибка при получении полного списка времени перерывов из базы данных."));
        }
    }

    public async Task<Result> GetReasonsForCancellationById(int id)
    {
        try
        {
            logger.LogInformation("Получение времени перерыва из базы данных.");
            var reason = await _dbContext.ReasonsForCancellations.FindAsync(id);
            if(reason != null)
            {
                logger.LogInformation("Время перерыва с id {TargetId} получено из базы данных.", id);
                return Result.Success(reason);
            }
            logger.LogError("Время перерыва с id {TargetId} не найдено в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Время перерыва с id {id} не найдено в базе данных."));
        }
        catch(Exception)
        {
            logger.LogError("Ошибка при получении времени перерыва с id {TargetId} из базы данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при получении времени перерыва с id {id} из базы данных."));
        }
    }

    public async Task<Result> UpdateAsync(int reasonId, int? recordId = null, string? explanation = null)
    {
        try
        {
            logger.LogInformation("Обновление времени перерыва в базе данных.");
            var reason = await _dbContext.ReasonsForCancellations.FindAsync(reasonId);
            if(reason != null)
            {
                reason.RecordId = recordId ?? reason.RecordId;
                reason.Explanation = explanation ?? reason.Explanation;
                await _dbContext.SaveChangesAsync();
                logger.LogInformation("Время перерыва с id {TargetActionId} обновлено в базе данных.", reasonId);
                return Result.Success();
            }
            logger.LogError("Время перерыва с id {TargetActionId} не найдено в базе данных.", reasonId);
            return Result.Failure(new Error(Errors.NotFound, $"Время перерыва с id {reasonId} не найдено в базе данных."));
        }
        catch(Exception )
        {
            logger.LogError("Ошибка при обновлении времени перерыва с id {TargetActionId} в базе данных.", reasonId);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при обновлении времени перерыва с id {reasonId} в базе данных."));
        }
    }
}
