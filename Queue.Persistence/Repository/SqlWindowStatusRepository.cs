using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;
using System;
using System.Data;

namespace Queue.Persistence.Repository;

public class SqlWindowStatusRepository(QueuesDbContext _dbContext, ILogger<SqlWindowStatusRepository> logger) : IWindowStatusRepository
{
    public async Task<Result> AddAsync(WindowStatus windowStatus)
    {
        try
        {
            logger.LogInformation("Добавление нового статуса окна в базу данных.");
            await _dbContext.WindowStatuses.AddAsync(windowStatus);
            await _dbContext.SaveChangesAsync();
            logger.LogInformation("Статус окна {TargetNameRu}({TargetNameKk}, {TargetNameEn}) добавлена в базу данных.", windowStatus.NameRu, windowStatus.NameKk, windowStatus.NameEn);
            return Result.Success();
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при добавлении статуса окна {TargetNameRu}({TargetNameKk}, {TargetNameEn}) в базу данных.", windowStatus.NameRu, windowStatus.NameKk, windowStatus.NameEn);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при добавлении статуса окна {windowStatus.NameRu}({windowStatus.NameKk}, {windowStatus.NameEn}) в базу данных."));
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            logger.LogInformation("Удаление статуса окна из базы данных.");
            var windowStatus = await _dbContext.WindowStatuses.FindAsync(id);
            if (windowStatus != null)
            {
                _dbContext.WindowStatuses.Remove(windowStatus);
                await _dbContext.SaveChangesAsync();
                logger.LogInformation("Статус окна с id {TargetId} удалена из базы данных.", id);
                return Result.Success();
            }
            logger.LogError("Статус окна с id {TargetId} не найдена в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Статус окна с id {id} не найдена в базе данных."));
        }
        catch(Exception) 
        {
            logger.LogError("Ошибка при удалении статуса окна с id {TargetId} из базы данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при удалении статуса окна с id {id} из базы данных."));
        }
        

    }


    public async Task<Result<List<WindowStatus>>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("Получение полного списка статуса окон из базы данных.");
            var windowStatuses=await _dbContext.WindowStatuses.ToListAsync();
            logger.LogInformation("Полный список статуса окон получен из базы данных.");
            return Result.Success(windowStatuses);
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при получении полного списка статуса окон из базы данных.");
            return Result.Failure<List<WindowStatus>>(new Error(Errors.InternalServerError, "Ошибка при получении полного списка статуса окон из базы данных."));
        }
    }

    public async Task<Result> GetWindowStatusById(int id)
    {
        try
        {
            logger.LogInformation("Получение статуса окна из базы данных.");
            var windowStatus = await _dbContext.WindowStatuses.FindAsync(id);
            if(windowStatus != null)
            {
                logger.LogInformation("Статус окна с id {TargetId} получена из базы данных.", id);
                return Result.Success(windowStatus);
            }
            logger.LogError("Статус окна с id {TargetId} не найдена в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Статус окна с id {id} не найдена в базе данных."));

        }
        catch(Exception)
        {
            logger.LogError("Ошибка при получении статуса окна с id {TargetId} из базы данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при получении статуса окна с id {id} из базы данных."));
        }
    }

    public async Task<Result> UpdateAsync(int windowStatusId, string? nameRu = null, string? nameKk = null, string? nameEn = null, string? descriptionRu = null, string? descriptionKk = null, string? descriptionEn = null)
    {
        try
        {
            logger.LogInformation("Обновление статуса окна в базе данных.");
            var windowStatus = await _dbContext.WindowStatuses.FindAsync(windowStatusId);
            if (windowStatus != null)
            {
                windowStatus.NameRu = nameRu ?? windowStatus.NameRu;
                windowStatus.NameKk = nameKk ?? windowStatus.NameKk;
                windowStatus.NameEn = nameEn ?? windowStatus.NameEn;
                windowStatus.DescriptionRu = descriptionRu ?? windowStatus.DescriptionRu;
                windowStatus.DescriptionKk = descriptionKk ?? windowStatus.DescriptionKk;
                windowStatus.DescriptionEn = descriptionEn ?? windowStatus.DescriptionEn;
                await _dbContext.SaveChangesAsync();
                logger.LogInformation("Статус окна с id {TargetId} обновлена в базе данных.", windowStatusId);
                return Result.Success(); 
            }
            logger.LogError("Статус окна с id {TargetId} не найдена в базе данных.", windowStatusId);
            return Result.Failure<Role>(new Error(Errors.NotFound, $"Статус окна с id {windowStatusId} не найдена в базе данных."));
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при обновлении статуса окна с id {TargetId} в базе данных.", windowStatusId);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при обновлении статуса окна с id {windowStatusId} в базе данных."));
        }
    }
}
