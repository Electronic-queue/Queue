using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;
using System;
using static System.Collections.Specialized.BitVector32;

namespace Queue.Persistence.Repository;

public class SqlWindowRepository(QueuesDbContext _dbContext, ILogger<SqlWindowRepository> logger) : IWindowRepository
{
    public async Task<Result> AddAsync(Window window)
    {
        try
        {
            logger.LogInformation("Добавление нового окна в базу данных.");
            await _dbContext.AddAsync(window);
            await _dbContext.SaveChangesAsync();
            logger.LogInformation("Окно {TargetNumber} добавлено в базу данных.", window.WindowNumber);
            return Result.Success();
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при добавлении окна {TargetNumber} в базу данных.", window.WindowNumber);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при добавлении нового окна {window.WindowNumber} в базу данных."));
        }

    }

    public async Task<Result> DeleteAsync(int id)
    {

        try
        {
            logger.LogInformation("Удаление окна из базы данных.");
            var window = await _dbContext.Windows.FindAsync(id);
            if (window != null)
            {
                _dbContext.Windows.Remove(window);
                await _dbContext.SaveChangesAsync();
                logger.LogInformation("Окно с id {TargetActionId} удалено из базы данных.", id);
                return Result.Success();

            }
            logger.LogError("Окно с id {TargetActionId} не найдено в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Окно с id {id} не найдено в базе данных."));

        }
        catch (Exception)
        {
            logger.LogError("Ошибка при удалении окна с id {TargetActionId} из базы данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при удалении окна с id {id} из базы данных."));
        }
    }

    public async Task<Result<List<Window>>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("Получение полного списка окон из базы данных."); 
            var window = await _dbContext.Windows.ToListAsync();
            logger.LogInformation("Полный список окон получен из базы данных.");
            return Result.Success(window);
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при получении полного списка окон из базы данных.");
            return Result.Failure<List<Window>>(new Error(Errors.InternalServerError, "Ошибка при получении полного списка окон из базы данных."));
        }

    }

    public async Task<Result> GetWindowById(int id)
    {
        try
        {
            logger.LogInformation("Получение окна из базы данных.");
            var window = await _dbContext.Windows.FindAsync(id);
            if (window != null)
            {
                logger.LogInformation("Окно с id {TargetId} получено из базы данных.", id);
                return Result.Success(window);
            }
            logger.LogError("Окно с id {TargetId} не найдено в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Окно с id {id} не найдено в базе данных."));
        }
        catch (Exception )
        {
            logger.LogError("Ошибка при получении окна с id {TargetId} из базы данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при получении окна с id {id} из базы данных."));
        }
    }

    public async Task<Result> UpdateAsync(int windowId, int? windowStatusId = null, int? windowNumber = null, int? createdBy = null)
    {

        try
        {
            logger.LogInformation("Обновление окна в базе данных.");
            var windowUpdate = await _dbContext.Windows.FindAsync(windowId);
            if (windowUpdate != null)
            {
                windowUpdate.WindowNumber = windowNumber ?? windowUpdate.WindowNumber;
                windowUpdate.WindowStatusId = windowStatusId ?? windowUpdate.WindowStatusId;
                windowUpdate.CreatedBy = createdBy ?? windowUpdate.CreatedBy;

                await _dbContext.SaveChangesAsync();
                logger.LogInformation("Окно с id {TargetId} обновлено в базе данных.", windowId);
                return Result.Success();
            }
            logger.LogError("Окно с id {TargetId} не найдено в базе данных.", windowId);
            return Result.Failure(new Error(Errors.NotFound, $"Окно с id {windowId} не найдено в базе данных."));

        }
        catch (Exception )
        {
            logger.LogError("Ошибка при обновлении окна с id {TargetId} в базе данных.", windowId);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при обновлении окна с id {windowId} в базе данных."));
        }
    }
}
