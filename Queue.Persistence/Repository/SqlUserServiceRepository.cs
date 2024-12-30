using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;
using System.IO;

namespace Queue.Persistence.Repository;

public class SqlUserServiceRepository(QueuesDbContext _dbContext, ILogger<SqlUserServiceRepository> logger) : IUserServiceRepository
{
    public async Task<Result> AddAsync(UserService userService)
    {
        try
        {
            logger.LogInformation("Добавление нового предоставления услуги в базу данных.");
            await _dbContext.UserServices.AddAsync(userService);
            await _dbContext.SaveChangesAsync();
            logger.LogInformation("Предоставление {TargetUserId} услуги  {TargetServiceId} добавлено в базу данных.", userService.UserId, userService.ServiceId);
            return Result.Success();
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при добавлении предоставления {TargetUserId} услуги {TargetServiceId} в базу данных.", userService.UserId, userService.ServiceId);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при добавлении предоставления {userService.UserId} услуги {userService.ServiceId} в базу данных."));
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            logger.LogInformation("Удаление предоставления услуги из базы данных.");
            var userService = await _dbContext.UserServices.FindAsync(id);
            if (userService != null)
            {
                _dbContext.Remove(userService);
                await _dbContext.SaveChangesAsync();
                logger.LogInformation("Предоставление услуги с id {TargetUserServiceId} удалено из базы данных.", id);
                return Result.Success();
            }
            logger.LogError("Предоставление услуги с id {TargetUserServiceId} не найдено в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Предоставление услуги с id {id} не найдено в базе данных."));
        }
        catch(Exception)
        {
            logger.LogError("Ошибка при удалении предоставления услуги с id {TargetUserServiceId} из базы данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при удалении предоставления услуги с id {id} из базы данных."));
        }
    }

    public async Task<Result<List<UserService>>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("Получение полного списка предоставлений услуг из базы данных.");
            var userService = await _dbContext.UserServices.ToListAsync();
            logger.LogInformation("Полный список предоставлений услуг получен из базы данных.");
            return Result.Success(userService);
        }
        catch (Exception )
        {
            logger.LogError("Ошибка при получении полного списка предоставлений услуг из базы данных.");
            return Result.Failure<List<UserService>>(new Error(Errors.InternalServerError, "Ошибка при получении полного списка предоставлений услуг из базы данных."));
        }
    }

    public async Task<Result> GetUserServiceById(int id)
    {
        try
        {
            logger.LogInformation("Получение предоставления услуги из базы данных.");
            var userService = await _dbContext.UserServices.FindAsync(id);
            if(userService != null)
            {
                 logger.LogInformation("Предоставление услуги с id {TargetId} получено из базы данных.", id);
                return Result.Success();
            }
            logger.LogError("Предоставление услуги с id {TargetId} не найдено в базе данных.", id);
            return Result.Failure<RoleAccess>(new Error(Errors.NotFound, $"Предоставление услуги с id {id} не найдено в базе данных."));
        }
        catch(Exception)
        {
            logger.LogError("Ошибка при получении предоставления услуги с id {TargetId} из базы данных.", id);
            return Result.Failure<RoleAccess>(new Error(Errors.InternalServerError, $"Ошибка при получении предоставления услуги с id {id} из базы данных."));
        }
    }

    public async Task<Result> UpdateAsync(int userServiceId, int? userId = null, int? serviceId = null, string? descriptionRu = null, string? descriptionKk = null, string? description = null, bool? isActive = null)
    {
        try
        {
            logger.LogInformation("Обновление предоставления услуги в базе данных.");
            var userService = await _dbContext.UserServices.FindAsync(userServiceId);
            if(userService != null)
            {
                userService.UserId = userId ?? userService.UserId;
                userService.ServiceId = serviceId ?? userService.ServiceId;
                userService.DescriptionRu = descriptionRu ?? userService.DescriptionRu;
                userService.DescriptionKk = descriptionKk ?? userService.DescriptionKk;
                userService.DescriptionEn = description ?? userService.DescriptionEn;
                userService.IsActive = isActive ?? userService.IsActive;
                await _dbContext.SaveChangesAsync();
                logger.LogInformation("Предоставление услуги с id {TargetId} обновлено в базе данных.", userServiceId);
                return Result.Success();
            }
            logger.LogError("Предоставление услуги с id {TargetId} не найдено в базе данных.", userServiceId);
            return Result.Failure(new Error(Errors.NotFound, $"Предоставление услуги с id {userServiceId} не найдено в базе данных."));
        }
        catch(Exception )
        {
            logger.LogError("Ошибка при обновлении предоставления услуги с id {TargetId} в базе данных.", userServiceId);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при обновлении предоставления услуги с id {userServiceId} в базе данных."));
        }

    }
}
