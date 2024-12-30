using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;
using System.IO;

namespace Queue.Persistence.Repository;

public class SqlUserWindowRepository(QueuesDbContext _dbContext, ILogger<SqlUserWindowRepository> logger) : IUserWindowRepository
{
    public async Task<Result> AddAsync(UserWindow userWindow)
    {
        try
        {
            logger.LogInformation("Добавление нового предоставления окна в базу данных.");
            await _dbContext.UserWindows.AddAsync(userWindow);
            await _dbContext.SaveChangesAsync();
            logger.LogInformation("Предоставление {TargetUserId} окна {TargetWindowId} добавлено в базу данных.", userWindow.UserId, userWindow.WindowId);
            return Result.Success();

        }
        catch (Exception )
        {
            logger.LogError("Ошибка при добавлении предоставления {TargetUserId} окна {TargetRoleId} в базу данных.", userWindow.UserId, userWindow.WindowId);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при добавлении предоставления {userWindow.UserId} окна {userWindow.WindowId} в базу данных."));
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            logger.LogInformation("Удаление предоставления окна из базы данных.");
            var userWindow = await _dbContext.UserWindows.FindAsync(id);
            if (userWindow != null)
            {
                _dbContext.UserWindows.Remove(userWindow);
                await _dbContext.SaveChangesAsync();
                logger.LogInformation("Предоставление окна с id {TargetUserWindowId} удалено из базы данных.", id);
                return Result.Success();
            }
            logger.LogError("Предоставление окна с id {TargetUserWindowId} не найдено в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Предоставление окна с id {id} не найдено в базе данных."));
        }
        catch(Exception)
        {
            logger.LogError("Ошибка при удалении предоставления окна с id {TargetUserWindowId} из базы данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при удалении предоставления окна с id {id} из базы данных."));
        }
    }

    public async Task<Result<List<UserWindow>>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("Получение полного списка предоставлений окон из базы данных.");
            var userWindow = await _dbContext.UserWindows.ToListAsync();
            logger.LogInformation("Полный список предоставлений окон получен из базы данных.");
            return Result.Success(userWindow);
        }
        catch(Exception)
        {
            logger.LogError("Ошибка при получении полного списка предоставлений окон из базы данных.");
            return Result.Failure<List<UserWindow>>(new Error(Errors.InternalServerError, "Ошибка при получении полного списка предоставлений окон из базы данных."));
        }
    }

    public async Task<Result> GetUserWindowById(int id)
    {
        try
        {
            logger.LogInformation("Получение предоставления окна из базы данных.");
            var userWindow = await _dbContext.UserWindows.FindAsync(id);
            if(userWindow != null)
            {
                logger.LogInformation("Предоставление окна с id {TargetId} получено из базы данных.", id);
                return Result.Success();
            }
            logger.LogError("Предоставление окна с id {TargetId} не найдено в базе данных.", id);
            return Result.Failure<RoleAccess>(new Error(Errors.NotFound, $"Предоставление окна с id {id} не найдено в базе данных."));
        }
        catch(Exception)
        {
            logger.LogError("Ошибка при получении предоставления окна с id {TargetId} из базы данных.", id);
            return Result.Failure<RoleAccess>(new Error(Errors.InternalServerError, $"Ошибка при получении предоставления окна с id {id} из базы данных."));
        }
    }

    public async Task<Result> UpdateAsync(int userWindowId, int? userId = null, int? windowId = null)
    {
        try
        {
            logger.LogInformation("Обновление предоставления окна в базе данных.");
            var userWindow = await _dbContext.UserWindows.FindAsync(userWindowId);
            if(userWindow != null)
            {
                userWindow.UserId = userId ?? userWindow.UserId;
                userWindow.WindowId = windowId ?? userWindow.WindowId;

                await _dbContext.SaveChangesAsync();
                logger.LogInformation("Предоставление окна с id {TargetUserWindowId} обновлено в базе данных.", userWindowId);
                return Result.Success();
            }

            logger.LogError("Предоставление окна с id {TargetUserWindowId} не найдено в базе данных.", userWindowId);
            return Result.Failure(new Error(Errors.NotFound, $"Предоставление окна с id {userWindowId} не найдено в базе данных."));
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при обновлении предоставления окна с id {TargetUserWindowId} в базе данных.", userWindowId);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при обновлении предоставления окна с id {userWindowId} в базе данных."));
        }
    }
}
