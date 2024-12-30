using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;
using System.Data;

namespace Queue.Persistence.Repository;

public class SqlQueueTypeRepository(QueuesDbContext _dbContext, ILogger<SqlQueueTypeRepository> logger) : IQueueTypeRepository
{
    public async Task<Result> AddAsync(QueueType queueType)
    {

        try
        {
            logger.LogInformation("Добавление нового типа очереди в базу данных.");
            await _dbContext.QueueTypes.AddAsync(queueType);
            await _dbContext.SaveChangesAsync();
            logger.LogInformation("Тип очереди {TargetNameRu},{TargetNameKk}, {TargetNameEn} добавлена в базу данных.", queueType.NameRu, queueType.NameKk, queueType.NameEn);
            return Result.Success();
        }
        catch (Exception )
        {
            logger.LogError("Ошибка при добавлении типа очереди {TargetNameRu}({TargetNameKk}, {TargetNameEn}) в базу данных.", queueType.NameRu, queueType.NameKk, queueType.NameEn);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при добавлении типа очереди {queueType.NameRu}({queueType.NameKk}, {queueType.NameEn}) в базу данных."));
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            logger.LogInformation("Удаление типа очереди из базы данных.");
            var queueType = await _dbContext.QueueTypes.FindAsync(id);
            if(queueType != null)
            {
                _dbContext.QueueTypes.Remove(queueType);
                await _dbContext.SaveChangesAsync();
                logger.LogInformation("Тип очереди с id {TargetId} удалена из базы данных.", id);
                return Result.Success();
            }
            logger.LogError("Тип очереди с id {TargetId} не найдена в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Тип очереди с id {id} не найдена в базе данных."));

        }
        catch(Exception)
        {
            logger.LogError("Ошибка при удалении типа очереди с id {TargetRoleId} из базы данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при удалении ртипа очереди с id {id} из базы данных."));
        }
    }

    public async Task<Result<List<QueueType>>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("Получение полного списка типа очередей из базы данных.");
            var queueType = await _dbContext.QueueTypes.ToListAsync();
            logger.LogInformation("Полный список типа очередей получен из базы данных.");
            return Result.Success(queueType);
        }
        catch(Exception)
        {
            logger.LogError("Ошибка при получении полного списка типа очередей из базы данных.");
            return Result.Failure<List<QueueType>>(new Error(Errors.InternalServerError, "Ошибка при получении полного списка типа очередей из базы данных."));
        }
    }

    public async Task<Result> GetQueueTypedById(int id)
    {
        try
        {
            logger.LogInformation("Получение типа очереди из базы данных.");
            var queueType = await _dbContext.QueueTypes.FindAsync(id);
            if(queueType != null)
            {
                logger.LogInformation("Тип очереди с id {TargetId} получена из базы данных.", id);
                return Result.Success();
            }
            logger.LogError("Тип очереди с id {TargetId} не найдена в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Тип очереди с id {id} не найдена в базе данных."));
        }
        catch(Exception) 
        {
            logger.LogError("Ошибка при получении типа очереди с id {TargetRoleId} из базы данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при получении типа очереди с id {id} из базы данных."));
        }

    }

    public async Task<Result> UpdateAsync(int queueTypeId, string? nameRu = null, string? nameKk = null, string? nameEn = null, string? decriptionRu = null, string? decriptionKk = null, string? decriptionEn = null)
    {
        try
        {
            logger.LogInformation("Обновление  типа очереди в базе данных.");
            var queueType = await _dbContext.QueueTypes.FindAsync(queueTypeId);
            if (queueType != null)
            {
                queueType.NameRu = nameRu ?? queueType.NameRu;
                queueType.NameKk = nameKk ?? queueType.NameKk;
                queueType.NameEn = nameEn ?? queueType.NameEn;
                queueType.DescriptionRu = decriptionRu ?? queueType.DescriptionRu;
                queueType.DescriptionKk = decriptionKk ?? queueType.DescriptionKk;
                queueType.DescriptionEn = decriptionEn ?? queueType.DescriptionEn;
                await _dbContext.SaveChangesAsync();
                logger.LogInformation("Тип очереди с id {TargetId} обновлена в базе данных.", queueTypeId);
                return Result.Success();
            }
            logger.LogError("Тип очереди с id {TargetId} не найдена в базе данных.", queueTypeId);
            return Result.Failure<Role>(new Error(Errors.NotFound, $"Тип очереди с id {queueTypeId} не найдена в базе данных."));
        }
            
        catch(Exception)
        {
            logger.LogError("Ошибка при обновлении типа очереди с id {TargetId} в базе данных.", queueTypeId);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при обновлении типа очереди с id {queueTypeId} в базе данных."));
        }
        
    }
}
